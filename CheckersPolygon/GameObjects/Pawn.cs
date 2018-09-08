using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using CheckersPolygon.Controllers;

namespace CheckersPolygon.GameObjects
{
    /* Обычная шашка, ходит на 1 клетку, только в "своем" направлении, если только нет съедобных вариантов
     */
    [Serializable]
    class Pawn : BaseChecker
    {
        public Pawn(CheckerSide side, CheckerMoveDirection direction, CheckersCoordinateSet position) : base(side, direction, position)
        {
            this.TurnRange = 1; // Ходит только на 1 клетку
        }

        /* Получение списка возможных ходов шашки с учетом особенностей ее хода
         */
        public override PathPoint GetPossibleTurns(TurnDirection? bannedDirection)
        {
            // Находит возможные ходы с помощью универсального класса Pathfinder
            Pathfinder pathfinder = new Pathfinder();
            PathPoint turns = pathfinder.GetTurns(Position.boardPosition, this.TurnRange, Side, bannedDirection);
            // Ограничение хода с учетом "съедобных" ходов
            if (Direction == CheckerMoveDirection.Upstairs)
            {
                // Ограничение на ходы снизу
                if (turns[TurnDirection.BottomLeft].Count > 0)
                {
                    if (turns[TurnDirection.BottomLeft][0].finalPoint)
                        turns[TurnDirection.BottomLeft].Clear();
                }
                if (turns[TurnDirection.BottomRight].Count > 0)
                {
                    if (turns[TurnDirection.BottomRight][0].finalPoint)
                        turns[TurnDirection.BottomRight].Clear();
                }
            }
            else
            {
                // Ограничение на ходы сверху
                if (turns[TurnDirection.TopLeft].Count > 0)
                {
                    if (turns[TurnDirection.TopLeft][0].finalPoint)
                        turns[TurnDirection.TopLeft].Clear();
                }
                if (turns[TurnDirection.TopRight].Count > 0)
                {
                    if (turns[TurnDirection.TopRight][0].finalPoint)
                        turns[TurnDirection.TopRight].Clear();
                }
            }

            return turns;
        }

        /* Отрисовка шашки
         */
        public override void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
            graph.DrawEllipse(new Pen(Color.Black, 3),
                Position.screenPosition.X + Position.drawableSize.Width / 5, Position.screenPosition.Y + Position.drawableSize.Height / 5,
                Position.drawableSize.Width - (Position.drawableSize.Width / 5 * 2), Position.drawableSize.Height - (Position.drawableSize.Height / 5 * 2));

            if (selected) // Если шашка выбрана
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                    Position.screenPosition.X, Position.screenPosition.Y,
                    Position.drawableSize.Width, Position.drawableSize.Height);
        }
    }
}
