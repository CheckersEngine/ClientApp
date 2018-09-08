using CheckersPolygon.Controllers;
using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    /* Класс, отвечающий за нахождение возможных ходов для шашек
     */
    class Pathfinder
    {
        /* Получение списка доступных ходов для шашки в указаной позиции
         * Рекурсивный метод !
         */
        public PathPoint GetTurns(Point boardPosition, byte turnRange, CheckerSide side, TurnDirection? bannedDirection)
        {
            // Изначальный список ходов для данной позиции
            PathPoint turnPathPoint = new PathPoint(boardPosition);

            // Позиция
            Point position = boardPosition;
            bool nonPacifist = false; // Есть съедобные варианты
            Point[] directionSigns = new Point[4]
                {
                    GetDirectionSigns(TurnDirection.TopLeft),
                    GetDirectionSigns(TurnDirection.TopRight),
                    GetDirectionSigns(TurnDirection.BottomLeft),
                    GetDirectionSigns(TurnDirection.BottomRight)
                };

            // Попытка найти путь с учетом заблокированных направлений
            if (bannedDirection == null)
            {
                // Трассировка по всем направлениям с целью выяснить, есть ли "агрессивные" ходы
                PathPoint[] result = new PathPoint[4];
                result[0] = TraceSide(turnRange, side, position, TurnDirection.TopLeft);
                result[1] = TraceSide(turnRange, side, position, TurnDirection.TopRight);
                result[2] = TraceSide(turnRange, side, position, TurnDirection.BottomLeft);
                result[3] = TraceSide(turnRange, side, position, TurnDirection.BottomRight);

                // Есть ли "агрессивные варианты"
                for (int i = 0; i < 4; i++)
                    if (!result[i].IsOnlyFinalTraces())
                        nonPacifist = true;

                // Фильтрация ходов с учетом их возможной "агрессивности"
                for (int i = 0; i < 4; i++)
                    if (!nonPacifist)
                    {
                        if (!result[i].IsDeadEnd()) // Все ходы
                            turnPathPoint[i].AddRange(result[i][i]);
                    }
                    else
                    {
                        if (!result[i].IsOnlyFinalTraces(i)) // Только агрессивные ходы
                            turnPathPoint[i].AddRange(result[i][i]);
                    }
            }
            else
            {
                List<PathPoint> paths = new List<PathPoint>();
                // Трассировка по не заблокированным направлениям с целью выяснить, есть ли "агрессивные" ходы
                if (bannedDirection != TurnDirection.TopLeft)
                    paths.Add(TraceSide(turnRange, side, position, TurnDirection.TopLeft));

                if (bannedDirection != TurnDirection.TopRight)
                    paths.Add(TraceSide(turnRange, side, position, TurnDirection.TopRight));

                if (bannedDirection != TurnDirection.BottomLeft)
                    paths.Add(TraceSide(turnRange, side, position, TurnDirection.BottomLeft));

                if (bannedDirection != TurnDirection.BottomRight)
                    paths.Add(TraceSide(turnRange, side, position, TurnDirection.BottomRight));

                // Есть ли "агрессивные варианты"
                foreach (PathPoint point in paths)
                    if (!point.IsOnlyFinalTraces())
                        nonPacifist = true;

                // Фильтрация ходов с учетом их возможной "агрессивности" и заблокированных направлений

                // Фильтрация левого верхнего направления
                if (bannedDirection != TurnDirection.TopLeft)
                {
                    PathPoint result = TraceSide(turnRange, side, position, TurnDirection.TopLeft);
                    if (!nonPacifist)
                    {
                        if (!result.IsDeadEnd()) // Все ходы
                            turnPathPoint[TurnDirection.TopLeft].AddRange(result[TurnDirection.TopLeft]);
                    }
                    else
                    {
                        if (!result.IsOnlyFinalTraces()) // Только агрессивные ходы
                            turnPathPoint[TurnDirection.TopLeft].AddRange(result[TurnDirection.TopLeft]);
                    }
                }
                // Фильтрация правого верхнего направления
                if (bannedDirection != TurnDirection.TopRight)
                {
                    PathPoint result = TraceSide(turnRange, side, position, TurnDirection.TopRight);
                    if (!nonPacifist)
                    {
                        if (!result.IsDeadEnd()) // Все ходы
                            turnPathPoint[TurnDirection.TopRight].AddRange(result[TurnDirection.TopRight]);
                    }
                    else
                    {
                        if (!result.IsOnlyFinalTraces()) // Только агрессивные ходы
                            turnPathPoint[TurnDirection.TopRight].AddRange(result[TurnDirection.TopRight]);
                    }
                }
                // Фильтрация левого нижнего направления
                if (bannedDirection != TurnDirection.BottomLeft)
                {
                    PathPoint result = TraceSide(turnRange, side, position, TurnDirection.BottomLeft);
                    if (!nonPacifist)
                    {
                        if (!result.IsDeadEnd()) // Все ходы
                            turnPathPoint[TurnDirection.BottomLeft].AddRange(result[TurnDirection.BottomLeft]);
                    }
                    else
                    {
                        if (!result.IsOnlyFinalTraces()) // Только агрессивные ходы
                            turnPathPoint[TurnDirection.BottomLeft].AddRange(result[TurnDirection.BottomLeft]);
                    }
                }
                // Фильтрация правого нижнего направления
                if (bannedDirection != TurnDirection.BottomRight)
                {
                    PathPoint result = TraceSide(turnRange, side, position, TurnDirection.BottomRight);
                    if (!nonPacifist)
                    {
                        if (!result.IsDeadEnd()) // Все ходы
                            turnPathPoint[TurnDirection.BottomRight].AddRange(result[TurnDirection.BottomRight]);
                    }
                    else
                    {
                        if (!result.IsOnlyFinalTraces()) // Только агрессивные ходы
                            turnPathPoint[TurnDirection.BottomRight].AddRange(result[TurnDirection.BottomRight]);
                    }
                }
            }

            return turnPathPoint;
        }


        /* Трассировка диагонали от определенной позиции
         */
        private PathPoint TraceSide(byte turnRange, CheckerSide side, Point position, TurnDirection direction)
        {
            PathPoint path = new PathPoint(position);
            Point directionSigns = GetDirectionSigns(direction); // Вектор направления
            // Флаги
            bool pacifistFlag = true; // Присутствуют только "мирные" ходы
            bool eatableFlag = false; // Найдено съедобное
            bool obstacleFlag = false; // Найдено препятствие
            bool notFinished = false; // За съедобной шашкой не находится препятствие
            Point eatablePosition = new Point(-1, -1); // Позиция съедобной шашки
            byte turnLength = 1; // Длина хода, на которую ушла трассировка

            // Главный цикл трассировки
            // Позиция должна держаться в пределах доски и не превышать длины хода шашки
            while (turnLength <= turnRange &&
                                position.X + turnLength * directionSigns.X >= 0 && position.X + turnLength * directionSigns.X < 8 &&
                                position.Y + turnLength * directionSigns.Y >= 0 && position.Y + turnLength * directionSigns.Y < 8)
            {
                // Позиция с учетом коррекции координат
                Point newPosition = new Point(position.X + turnLength * directionSigns.X, position.Y + turnLength * directionSigns.Y);

                // Попытка найти препятствие на новой позиции
                // Препятствие --> своя пешка
                foreach (BaseChecker checker in side == CheckerSide.White ?
                    Game.gameplayController.state.whiteCheckers :
                    Game.gameplayController.state.blackCheckers)
                {
                    if (newPosition == checker.Position.boardPosition)
                    {
                        obstacleFlag = true; // Если препятствие найдено, сразу об этом сообщаем
                        break;
                    }
                }

                // Попытка найти врага на новой позиции
                foreach (BaseChecker checker in side == CheckerSide.White ?
                    Game.gameplayController.state.blackCheckers :
                    Game.gameplayController.state.whiteCheckers)
                {
                    if (newPosition == checker.Position.boardPosition)
                    {
                        if (eatableFlag)
                        {
                            // Если 2 врага подряд - значит несъедобные
                            obstacleFlag = true;
                            break;
                        }
                        else
                        {
                            // Если враг - сообщаем об этом
                            eatableFlag = true;
                            break;
                        }
                    }
                }

                // Если наткнулись на препятствие - сразу перестаем искать
                if (obstacleFlag)
                {
                    break;
                }

                // Если нашли съедобную шашку
                if (eatableFlag)
                {
                    // Позиция следующей за съедобной клетки
                    Point tracePoint = new Point(newPosition.X + directionSigns.X, newPosition.Y + directionSigns.Y);
                    if (tracePoint.X >= 0 && tracePoint.X < 8 &&
                        tracePoint.Y >= 0 && tracePoint.Y < 8)
                    {
                        // Проверка следующей за съедобной клетки на наличие шашки
                        foreach (BaseChecker checker in Game.gameplayController.state.whiteCheckers)
                        {
                            if (tracePoint == checker.Position.boardPosition)
                            {
                                notFinished = true;
                            }
                        }

                        // Проверка следующей за съедобной клетки на наличие шашки
                        foreach (BaseChecker checker in Game.gameplayController.state.blackCheckers)
                        {
                            if (tracePoint == checker.Position.boardPosition)
                            {
                                notFinished = true;
                            }
                        }

                        // Если следующей шашки нет - шашка съедобна
                        if (!notFinished)
                        {
                            // Трассировка со "съедобной" позиции с целью определить возможность продолжения текущего хода
                            // Рассматриваются все направления, кроме направления, обратного к вектору текущего хода
                            eatablePosition = tracePoint;
                            PathPoint possiblePath = GetTurns(eatablePosition, turnRange, side, OppositeDirection(direction));
                            possiblePath.afterAggression = true;
                            path[direction].Add(possiblePath);
                            pacifistFlag = false; // Ход перестает быть "мирным" - найден съедобный вариант
                        }
                    }
                }
                turnLength++;
            }

            // Нахождение "мирных" вариантов хода
            // Только если "агрессивных" ходов не обнаружено
            // Просмотр ведется в обратную сторону - от препятствия до ходящей шашки
            turnLength--;
            if (pacifistFlag)
            {
                int reverseLength = turnLength; // Длина обратного хода
                if (reverseLength > 1 && eatableFlag) // Если на текущая позиция соответствует препятствию
                    reverseLength--; // Уменьшить длину обратного хода

                // Попытка найти все возможные позиции для хода
                while (turnLength - reverseLength != turnLength
                    && position.X + reverseLength * directionSigns.X >= 0 && position.X + reverseLength * directionSigns.X < 8 &&
                                position.Y + reverseLength * directionSigns.Y >= 0 && position.Y + reverseLength * directionSigns.Y < 8)
                {
                    // Новая позиция
                    Point newPosition = new Point(position.X + reverseLength * directionSigns.X, position.Y + reverseLength * directionSigns.Y);
                    // Попытка найти соответствие какой-либо белой шашки заданной позиции
                    foreach (BaseChecker checker in Game.gameplayController.state.whiteCheckers)
                    {
                        if (newPosition == checker.Position.boardPosition)
                        {
                            return path;
                        }
                    }

                    // Попытка найти соответствие какой-либо черной шашки заданной позиции
                    foreach (BaseChecker checker in Game.gameplayController.state.blackCheckers)
                    {
                        if (newPosition == checker.Position.boardPosition)
                        {
                            return path;
                        }
                    }

                    // Добавление новой позиции для хода, если не обноружено столкновение с другой шашкой
                    path[direction].Add(new PathPoint(newPosition, true));
                    reverseLength--;
                }
            }

            return path;
        }

        /* Получение вектора направления по его типу перечисления (enum)
         */
        private Point GetDirectionSigns(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.TopLeft:
                    return new Point(-1, -1);
                case TurnDirection.TopRight:
                    return new Point(1, -1);
                case TurnDirection.BottomLeft:
                    return new Point(-1, 1);
                case TurnDirection.BottomRight:
                default:
                    return new Point(1, 1);
            }
        }

        /* Определение противоположного направления
         */
        private TurnDirection OppositeDirection(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.TopLeft:
                    return TurnDirection.BottomRight;
                case TurnDirection.TopRight:
                    return TurnDirection.BottomLeft;
                case TurnDirection.BottomLeft:
                    return TurnDirection.TopRight;
                case TurnDirection.BottomRight:
                default:
                    return TurnDirection.TopLeft;
            }
        }
    }
}
