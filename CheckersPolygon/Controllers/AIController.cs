using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Controllers
{
    /* Контроллер искусственного интеллекта
     * Отвечает за расчет хода компьютера
     */
    class AIController
    {
        // Сторона, за которую играет компьютер
        public CheckerSide side { get; private set; }

        public AIController(CheckerSide aiSide)
        {
            this.side = aiSide;
        }

        /* Расчет хода компьютера
         */
        public void GetAiTurn(out BaseChecker selectedChecker, out PathPoint toPosition)
        {
            selectedChecker = null;
            toPosition = null;
            List<PathPoint> possibleTurns = new List<PathPoint>(); // Список возможных ходов

            // Построение списка возможных ходов
            foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                Game.gameplayController.state.blackCheckers)
            {
                PathPoint turn = checker.GetPossibleTurns();
                if (!turn.IsDeadEnd()) // Если не тупиковая точка, добавляем маршрут в список
                    possibleTurns.Add(turn);
            }

            // Если ходы не найдены - проигрыш
            if (possibleTurns.Count == 0)
            {
                selectedChecker = null;
                toPosition = null;
                return;
            }

            // Попытка найти агрессивные ходы
            foreach (PathPoint chain in possibleTurns)
            {
                List<int> eatableDirections = chain.TryGetAggresiveDirections();
                if (eatableDirections.Count > 0)
                {
                    // Агрессивный ход найден
                    toPosition = chain;

                    // Выясняем принадлежность хода шашке и выходим из метода
                    foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                        Game.gameplayController.state.blackCheckers)
                    {
                        PathPoint turn = checker.GetPossibleTurns();
                        if (turn.Position == chain.Position)
                        {
                            selectedChecker = checker;
                            return;
                        }
                    }
                }
            }
            // "Съедобных" ходов нет, ищем мирные случайным образом
            Random rnd = new Random();
            toPosition = possibleTurns[rnd.Next(possibleTurns.Count - 1)];
            // Сопоставляем ход с шашкой
            foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                Game.gameplayController.state.blackCheckers)
            {
                PathPoint turn = checker.GetPossibleTurns();
                if (turn.Position == toPosition.Position)
                {
                    selectedChecker = checker;
                }
            }

            return;
        }

        /* Выбирает случайное напраление хода из доступных
         * null - если ни одного варианта хода нет
         */
        public PathPoint RandomPath(PathPoint point)
        {
            PathPoint toPosition = null;
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                if (point[i].Count > 0)
                {
                    toPosition = point[i][rnd.Next(point[i].Count - 1)];
                    return toPosition;
                }
            }

            return toPosition;
        }
    }
}
