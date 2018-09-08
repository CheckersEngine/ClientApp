using CheckersPolygon.Forms;
using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* Класс, контролирующий весь процесс игры
     * Включает в себя:
     * - Первоначальную расстановку шашек при начале новой игры
     * - Хранение, изменение, загрузку и сохранение состояния игровых обьектов (state)
     * - Расчет хода на основе данных о взаимном расположении шашек на доске
     * - Управление ходом игрока и компьютера
     * - Обработка событий в соответствии с правилами игры 
     */
    internal sealed class GameplayController
    {
        public BoardCell[,] board = new BoardCell[8, 8]; // Клетки игровой доски (активный игровой объект)
        public BoardMarker[,] markers = new BoardMarker[2, 8]; // Метки клеток игровой доски (1,2,3,4.../A,B,C,D...)
        public GameState state = new GameState(); // Состояние игровых обьектов (Шашек и др.)
        public Size cellSize; // Размер ячейки (для поддержки масштабирования)
        private Panel gameBoard; // Ссылка на игровое поле на форме


        /* Первоначальная расстановка всех игровых объектов
         * Регистрация их в списке отрисовки
         */
        public GameplayController(ref Panel gameBoard, bool phase, bool aiAffected)
        {
            this.state.phase = phase;
            this.gameBoard = gameBoard;

            // Если намечается игра с компьютером
            if (aiAffected)
            {
                state.isAiControlled = true;
                state.aiSide = phase == true ? CheckerSide.White : CheckerSide.Black;
            }

            // Установка размера одной клетки
            cellSize.Width = gameBoard.Size.Width / 8;
            cellSize.Height = gameBoard.Size.Height / 8;

            // Инициализация клеток доски
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CheckersCoordinateSet position = new CheckersCoordinateSet(
                        new Point(j * cellSize.Width, i * cellSize.Height),
                        cellSize,
                        new Point(j, i));

                    board[j, i] = new BoardCell(((i + j) % 2 == 0) ? false : true,
                        position);

                    Game.drawingController.AddToDrawingList(board[j, i]);
                }
            }

            // Инициализация маркеров клеток (A, B, C, D ...)
            for (int i = 0; i < 8; i++)
            {
                // Вертикальные маркеры
                CheckersCoordinateSet position = new CheckersCoordinateSet(
                        new Point(2, i * cellSize.Height + (cellSize.Height / 2) - (cellSize.Height / 6)),
                        new Size(12, 12),
                        new Point(0, i));
                markers[0, i] = new BoardMarker(Convert.ToChar('8' - i), position);
                Game.drawingController.AddToDrawingList(markers[0, i]);

                // Горизонтальные маркеры
                position = new CheckersCoordinateSet(
                        new Point(i * cellSize.Width + (cellSize.Width / 2) - (cellSize.Width / 6), 8 * cellSize.Height - 18),
                        new Size(12, 12),
                        new Point(i, 0));
                markers[1, i] = new BoardMarker(Convert.ToChar('A' + i), position);
                Game.drawingController.AddToDrawingList(markers[1, i]);
            }

            // Первоначальная расстановка шашек
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Определение координат и размеров шашки, находящейся сверху
                    CheckersCoordinateSet positionTop = new CheckersCoordinateSet(
                        new Point((2 * j + ((i + 1) % 2)) * cellSize.Width, i * cellSize.Height),
                        cellSize,
                        new Point(2 * j + ((i + 1) % 2), i));

                    // Определение координат и размера шашки, находящейся снизу
                    CheckersCoordinateSet positionBottom = new CheckersCoordinateSet(
                        new Point(7 * cellSize.Width - (((2 * j + ((i + 1) % 2)) * cellSize.Width)),
                        7 * cellSize.Height - i * cellSize.Height),
                        cellSize,
                        new Point(7 - (2 * j + ((i + 1) % 2)), 7 - i));

                    // Инициализация белой шашки
                    BaseChecker checker = new Pawn(CheckerSide.White,
                        phase == true ? CheckerMoveDirection.Downstairs : CheckerMoveDirection.Upstairs,
                        phase == true ? positionTop : positionBottom);
                    state.whiteCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);

                    // Инициализация черной шашки
                    checker = new Pawn(CheckerSide.Black,
                        phase == false ? CheckerMoveDirection.Downstairs : CheckerMoveDirection.Upstairs,
                        phase == false ? positionTop : positionBottom);
                    state.blackCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);
                }
            }

            // Ход искуственного интеллекта
            Game.drawingController.PrioritizedDraw();
        }

        /* Изменение всех размеров всех игровых объектов
         * Перерасстановка их на доске
         */
        public void OnResize()
        {
            // Установка размера одной клетки
            cellSize.Width = gameBoard.Size.Width / 8;
            cellSize.Height = gameBoard.Size.Height / 8;

            // Изменение размера и координат клеток доски
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[j, i].Position.screenPosition = new Point(j * cellSize.Width, i * cellSize.Height);
                    board[j, i].Position.drawableSize = cellSize;
                }
            }

            // Изменение размера и координат маркеров доски
            for (int i = 0; i < 8; i++)
            {
                markers[0, i].Position.screenPosition = new Point(0,
                    i * cellSize.Height + (cellSize.Height / 2) - (cellSize.Height / 8));
                markers[1, i].Position.screenPosition = new Point(i * cellSize.Width + (cellSize.Width / 2) - (cellSize.Width / 8),
                    8 * cellSize.Height - 18);
            }

            // Изменение размера и координат белых шашек
            foreach (BaseChecker checker in state.whiteCheckers)
            {
                checker.Position = new CheckersCoordinateSet(
                    new Point(checker.Position.boardPosition.X * cellSize.Width, checker.Position.boardPosition.Y * cellSize.Height),
                    cellSize,
                    new Point(checker.Position.boardPosition.X, checker.Position.boardPosition.Y));
            }

            // Изменение размера и координат черных шашек
            foreach (BaseChecker checker in state.blackCheckers)
            {
                checker.Position = new CheckersCoordinateSet(
                    new Point(checker.Position.boardPosition.X * cellSize.Width, checker.Position.boardPosition.Y * cellSize.Height),
                    cellSize,
                    new Point(checker.Position.boardPosition.X, checker.Position.boardPosition.Y));
            }

            // Перерисовка изображения
            Game.drawingController.PrioritizedDraw();
        }

        #region Расчет хода и передвижение шашек

        /* При нажатии на игровую доску вызывается данный метод
         */
        public void OnClick(MouseEventArgs mouseArgs)
        {
            bool anyoneSelected = false;
            BaseChecker previouslySelectedChecker = null;
            BaseChecker currentSelectedChecker = null;

            // Попытка найти выбранную шашку
            anyoneSelected = TryFindSelectedCheckers(mouseArgs, ref previouslySelectedChecker, ref currentSelectedChecker);

            // Если шашка не была выбрана
            if (!anyoneSelected)
            {
                // Если на прошлом шаге была выбрана шашка 
                if (previouslySelectedChecker != null)
                {
                    // Попытка хода на заданную позицию
                    Point toBoardPosition = new Point()
                    {
                        X = mouseArgs.X / cellSize.Width,
                        Y = mouseArgs.Y / cellSize.Height
                    };
                    DoMovement(previouslySelectedChecker, toBoardPosition);

                    // Проверка условий победы и поражения
                    CheckVictoryConditions();
                }
                else
                {
                    // Кликнул на пустое поле
                    DeselectAllCheckers();
                    state.activeChecker = null;
                    state.activeCheckerPathPoint = null;
                }
            }
            else
            {
                // Выбрана шашка
                // Предотвратить ход другой шашкой (кроме выбранной)
                if (state.activeChecker != null)
                    if (state.activeChecker != currentSelectedChecker)
                    {
                        currentSelectedChecker = null;
                        return;
                    }

                List<BaseChecker> turnList = new List<BaseChecker>();
                bool aggressive = false;
                bool turnFound = false;
                foreach (BaseChecker checker in currentSelectedChecker.Side == CheckerSide.White ? state.whiteCheckers : state.blackCheckers)
                {
                    PathPoint turns = checker.GetPossibleTurns();
                    if (turns.IsDeadEnd()) continue;
                    if (turns.TryGetAggresiveDirections().Count > 0)
                    {
                        turnList.Add(checker);
                        aggressive = true;
                    }
                }

                // Фильтрация ходов (если есть возможность что то съесть, заблокировать "несъедобные" ходы)
                if (!aggressive)
                {
                    ClearHighlighting();
                    HighlightPossibleTurns(currentSelectedChecker); // Подсветить ее
                }
                else
                {
                    foreach (BaseChecker checker in turnList)
                        if (checker == currentSelectedChecker)
                        {
                            turnFound = true;
                        }

                    ClearHighlighting();
                    if (turnFound)
                    {
                        HighlightPossibleTurns(currentSelectedChecker);
                    }
                    else
                    {
                        DeselectAllCheckers();
                    }
                }
            }

            // Перерисовать сцену
            Game.drawingController.PrioritizedDraw();

            // Очистка памяти от объектов, порожденных активным вычислением съедобных вариантов
            GC.Collect();
        }

        /* Ход искуственного интеллекта
         * Расчет ходов, проверка условий выигрыша и поражения
         */
        public void AiTurn()
        {
            BaseChecker selectedChecker = null;
            PathPoint toPoint;
            AIController ai = new AIController(state.aiSide);

            // Попытка найти выбранную шашку
            ai.GetAiTurn(out selectedChecker, out toPoint);

            // Если шашка была выбрана
            if (selectedChecker != null)
            {
                // Попытка хода на заданную позицию
                do
                {
                    PathPoint newPath = ai.RandomPath(toPoint);
                    if (newPath == null)
                    {
                        break;
                    }

                    Point toBoardPosition = newPath.Position;
                    DoMovement(selectedChecker, toBoardPosition);
                    toPoint = newPath;
                }
                while (state.activeChecker != null);
                // Проверка условий победы и поражения
                CheckVictoryConditions();

            }
            else
            {
                // Проверка условий победы и поражения
                CheckVictoryConditions();
            }

            // Перерисовать сцену
            Game.drawingController.PrioritizedDraw();

            // Очистка памяти от объектов, порожденных активным вычислением съедобных вариантов
            GC.Collect();
        }

        /* Проверка условий выигрыша/проигрыша всех сторон
         */
        private void CheckVictoryConditions()
        {
            bool toilet = true;
            if (state.whiteCheckers.Count == 0)
            {
                // Победа черных
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage("Черные победили!");
                FormVictory victoryDialog = new FormVictory(false, false);
                victoryDialog.ShowDialog();
            }
            else if (state.blackCheckers.Count == 0)
            {
                // Победа белых
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage("Белые победили!");
                FormVictory victoryDialog = new FormVictory(true, false);
                victoryDialog.ShowDialog();
            }
            else
            {
                // Проверка на "сортир"
                foreach (BaseChecker checker in state.turn == CheckerSide.White ? state.whiteCheckers : state.blackCheckers)
                {
                    if (!checker.GetPossibleTurns().IsDeadEnd())
                        toilet = false;
                }

                // Если поставлен "сортир"
                if (toilet)
                {
                    if (state.turn == CheckerSide.White)
                    {
                        // Сортир белым
                        Game.userLogController.WriteMessage("Белые позорно проиграли!");
                        FormVictory victoryDialog = new FormVictory(true, true);
                        victoryDialog.ShowDialog();
                    }
                    else
                    {
                        // Сортир черным
                        Game.userLogController.WriteMessage("Черные позорно проиграли!");
                        FormVictory victoryDialog = new FormVictory(false, true);
                        victoryDialog.ShowDialog();
                    }
                }
            }
        }

        /* Попытка найти выделенные шашки после нажатия кнопки мыши
         */
        private bool TryFindSelectedCheckers(MouseEventArgs mouseArgs, ref BaseChecker previouslySelectedChecker, ref BaseChecker currentSelectedChecker)
        {
            bool anyoneSelected = false;
            if (state.turn == CheckerSide.White) // Соблюдение очередности хода
            {
                foreach (BaseChecker whiteChecker in state.whiteCheckers)
                {
                    // Пометка прежде выбранной шашки
                    if (whiteChecker.selected)
                        previouslySelectedChecker = whiteChecker;

                    // Пометка только что выбранной шашки
                    Rectangle checkerCoord = new Rectangle(whiteChecker.Position.screenPosition, whiteChecker.Position.drawableSize);
                    if (checkerCoord.Contains(mouseArgs.Location))
                    {
                        SelectChecker(whiteChecker);
                        currentSelectedChecker = whiteChecker;
                        anyoneSelected = true;
                    }
                }
            }
            else
            {
                foreach (BaseChecker blackChecker in state.blackCheckers)
                {
                    // Пометка прежде выбранной шашки
                    if (blackChecker.selected)
                        previouslySelectedChecker = blackChecker;

                    // Пометка только что выбранной шашки
                    Rectangle checkerCoord = new Rectangle(blackChecker.Position.screenPosition, blackChecker.Position.drawableSize);
                    if (checkerCoord.Contains(mouseArgs.Location))
                    {
                        SelectChecker(blackChecker);
                        currentSelectedChecker = blackChecker;
                        anyoneSelected = true;
                    }
                }
            }
            return anyoneSelected;
        }


        /* Попытка движения шашки
         * Расчет хода
         * ------------------------------------------------------------------------------
         */
        private void DoMovement(BaseChecker previouslySelectedChecker, Point toPosition)
        {
            string logMessage = "Ход ";
            PathPoint turns;
            // Снятие выделения с шашек
            DeselectAllCheckers();

            // Если ход не начался
            if (state.activeCheckerPathPoint == null)
            {
                turns = previouslySelectedChecker.GetPossibleTurns(); // Расчет всех возможных цепочек ходов для шашки
                state.activeCheckerPathPoint = turns;
            }
            else
                turns = state.activeCheckerPathPoint; // Продолжение хода

            // Если позиция не тупиковая
            if (!turns.IsDeadEnd())
            {
                state.activeChecker = previouslySelectedChecker;

                // Ход
                for (int i = 0; i < 4; i++)
                    foreach (PathPoint point in turns[i])
                    {
                        if (toPosition == point.Position)
                        {
                            Point fromPosition = new Point()
                            {
                                X = previouslySelectedChecker.Position.boardPosition.X,
                                Y = previouslySelectedChecker.Position.boardPosition.Y
                            };
                            logMessage += previouslySelectedChecker.GetPrintablePosition() + " на ";
                            // Перемещение шашки на выбранную позицию
                            previouslySelectedChecker.MoveTo(new Point(toPosition.X, toPosition.Y));
                            logMessage += previouslySelectedChecker.GetPrintablePosition();
                            Game.userLogController.WriteMessage(logMessage);
                            // Определение направления хода
                            Point moveDirection = new Point()
                            {
                                X = toPosition.X - fromPosition.X > 0 ? 1 : -1,
                                Y = toPosition.Y - fromPosition.Y > 0 ? 1 : -1
                            };

                            // Определение съеденных шашек
                            TryEatEnemyCheckers(previouslySelectedChecker, toPosition, fromPosition, moveDirection);

                            // Если шашка может стать дамкой
                            if (previouslySelectedChecker is Pawn && previouslySelectedChecker.Position.boardPosition.Y ==
                                (previouslySelectedChecker.Direction == CheckerMoveDirection.Upstairs ? 0 : 7))
                            {
                                // Замена шашки дамкой
                                King newKing = new King(previouslySelectedChecker.Side, previouslySelectedChecker.Position);
                                if (previouslySelectedChecker.Side == CheckerSide.White)
                                {
                                    state.whiteCheckers.Remove(previouslySelectedChecker);
                                    state.whiteCheckers.Add(newKing);
                                }
                                else
                                {
                                    state.blackCheckers.Remove(previouslySelectedChecker);
                                    state.blackCheckers.Add(newKing);
                                }

                                // Добавление в список отрисовки
                                Game.drawingController.AddToDrawingList(newKing);
                                previouslySelectedChecker.Destroy();

                                // Расчет дальнейшего пути для дамки
                                state.activeChecker = newKing;
                                if (state.activeCheckerPathPoint.afterAggression)
                                {
                                    state.activeCheckerPathPoint = newKing.GetPossibleTurns(PathPoint.GetOppositeDirection(GetDirection(moveDirection)));
                                    if (state.activeCheckerPathPoint.TryGetAggresiveDirections().Count == 0)
                                    {
                                        // Конец хода
                                        EndTurn();
                                        return;
                                    }
                                    else
                                    {
                                        DeselectAllCheckers();
                                        state.activeChecker.selected = true;
                                        HighlightPossibleTurns(state.activeChecker);
                                    }
                                }
                                else
                                {
                                    EndTurn();
                                    return;
                                }
                            }

                            // Найти ту точку, в которую он сходил
                            // Просчитать, возможно ли оттуда продолжение хода
                            foreach (PathPoint waypoint in state.activeCheckerPathPoint[GetDirection(moveDirection)])
                            {
                                if (waypoint.Position == toPosition)
                                {
                                    if (!waypoint.IsOnlyFinalTraces())
                                    {
                                        // Продолжение хода
                                        state.activeCheckerPathPoint = waypoint;
                                        DeselectAllCheckers();
                                        state.activeChecker.selected = true;
                                        HighlightPossibleTurns(state.activeChecker);
                                    }
                                    else
                                    {
                                        // Конец хода
                                        EndTurn();
                                    }
                                }
                            }
                            return;
                        }
                    }
            }
            else
            {
                // Тупиковая позиция, конец хода
                if (state.activeChecker != null || state.activeCheckerPathPoint != null)
                {
                    state.activeChecker = null;
                    state.activeCheckerPathPoint = null;
                }
            }
        }

        /* Конец хода, смена ходящего игрока
         */
        private void EndTurn()
        {
            state.activeChecker = null;
            state.activeCheckerPathPoint = null;

            if (state.turn == CheckerSide.White)
            {
                state.turn = CheckerSide.Black;
                Game.userLogController.WriteMessage("Ход черных");
            }
            else
            {
                state.turn = CheckerSide.White;
                Game.userLogController.WriteMessage("Ход белых");
            }

            // Если игра против компьютера
            if (state.isAiControlled)
            {
                Game.drawingController.PrioritizedDraw();
                // Ход компьютера
                if (state.aiSide == state.turn)
                    AiTurn();
            }
        }

        /* Попытка поедания всех встретившихся на пути шашек
         * Контроль за конечным результатом хода
         */
        private void TryEatEnemyCheckers(BaseChecker previouslySelectedChecker, Point toPosition, Point fromPosition, Point moveDirection)
        {
            List<BaseChecker> eatenCheckers = new List<BaseChecker>(); // Список всех съеденных шашек
            for (int i = fromPosition.X + moveDirection.X; i != toPosition.X; i += moveDirection.X)
            {
                for (int j = fromPosition.Y + moveDirection.Y; j != toPosition.Y; j += moveDirection.Y)
                {
                    // добавление всех встреченных за ход шашек противника в "съеденные"
                    if (previouslySelectedChecker.Side == CheckerSide.Black)
                    {
                        foreach (BaseChecker whiteChecker in state.whiteCheckers)
                        {
                            if (whiteChecker.Position.boardPosition.X == i &&
                                whiteChecker.Position.boardPosition.Y == j)
                            {
                                eatenCheckers.Add(whiteChecker);
                            }
                        }
                    }
                    else
                    {
                        foreach (BaseChecker blackChecker in state.blackCheckers)
                        {
                            if (blackChecker.Position.boardPosition.X == i &&
                                blackChecker.Position.boardPosition.Y == j)
                            {
                                eatenCheckers.Add(blackChecker);
                            }
                        }
                    }
                }
            }

            // Уничтожение всех съеденных шашек
            foreach (BaseChecker eatenChecker in eatenCheckers)
            {
                if (eatenChecker.Side == CheckerSide.White) state.whiteCheckers.Remove(eatenChecker);
                else state.blackCheckers.Remove(eatenChecker);

                eatenChecker.Destroy();
            }
            eatenCheckers.Clear();
        }

        /* Подсветка всех возможных ходов
         */
        private void HighlightPossibleTurns(BaseChecker currentSelectedChecker)
        {
            PathPoint turns = currentSelectedChecker.GetPossibleTurns();
            if (!turns.IsDeadEnd())
            {
                foreach (PathPoint point in turns[TurnDirection.TopLeft])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.TopRight])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.BottomLeft])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.BottomRight])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
            }
        }

        /* Отмена подсветки ячеек
         */
        private void ClearHighlighting()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[j, i].Highlighted = false;
        }

        /* Выбор шашки (включение подсветки)
         */
        private void SelectChecker(BaseChecker checker)
        {
            foreach (BaseChecker whiteChecker in state.whiteCheckers)
            {
                if (whiteChecker == checker)
                    whiteChecker.selected = true;
                else
                    whiteChecker.selected = false;
            }

            foreach (BaseChecker blackChecker in state.blackCheckers)
            {
                if (blackChecker == checker)
                    blackChecker.selected = true;
                else
                    blackChecker.selected = false;
            }
        }

        /* Отмена выбора шашек
         */
        private void DeselectAllCheckers()
        {
            foreach (BaseChecker whiteChecker in state.whiteCheckers)
                whiteChecker.selected = false;

            foreach (BaseChecker blackChecker in state.blackCheckers)
                blackChecker.selected = false;

            ClearHighlighting();
        }

        /* Определение направления по вектору
         */
        private TurnDirection GetDirection(Point directionVector)
        {
            if (directionVector.Y < 0)
            {
                if (directionVector.X < 0) return TurnDirection.TopLeft;
                else return TurnDirection.TopRight;
            }
            else
            {
                if (directionVector.X < 0) return TurnDirection.BottomLeft;
                else return TurnDirection.BottomRight;
            }
        }

        #endregion

        #region Сохранение и загрузка игры

        /* Сохранение состояние игры в файл, сериализация
         */
        public void SaveState(string filename)
        {
            FileStream stream = File.OpenWrite(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this.state);
            stream.Close();
        }

        /* Загрузка состояния игры из файла, десериализация
         */
        public void LoadState(string filename)
        {
            // Предварительная отвязка всех шашек от списка отрисовки
            // и удаление их из перечня шашек
            foreach (BaseChecker checker in state.whiteCheckers)
                checker.Destroy();
            foreach (BaseChecker checker in state.blackCheckers)
                checker.Destroy();
            state.whiteCheckers.Clear();
            state.blackCheckers.Clear();

            // Чтение состояния из файла
            FileStream stream = File.OpenRead(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            this.state = (GameState)formatter.Deserialize(stream);
            stream.Close();

            // Добавление шашек в список отрисовки
            foreach (BaseChecker checker in state.whiteCheckers)
                Game.drawingController.AddToDrawingList(checker);
            foreach (BaseChecker checker in state.blackCheckers)
                Game.drawingController.AddToDrawingList(checker);

            // Перерисовка сцены
            Game.drawingController.PrioritizedDraw();
        }
        #endregion
    }
}
