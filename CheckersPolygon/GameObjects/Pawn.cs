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
    /* Usual checker, walks on 1 cage, only forward, unless there are edible options
     */
    [Serializable]
    class Pawn : BaseChecker
    {
        public Pawn(CheckerSide side, CheckerMoveDirection direction, CheckersCoordinateSet position) : base(side, direction, position)
        {
            this.TurnRange = 1; // Only walks 1 cell
        }

        /* Obtaining a list of possible moves of the checker taking into account the features of its move
         */
        public override PathPoint GetPossibleTurns(TurnDirection? bannedDirection)
        {
            // Find possible moves using the universal Pathfinder class
            Pathfinder pathfinder = new Pathfinder();
            PathPoint turns = pathfinder.GetTurns(Position.boardPosition, this.TurnRange, Side, bannedDirection);
            // Restriction of a movement taking into account "edible" moves
            if (Direction == CheckerMoveDirection.Upstairs)
            {
                // Restriction on bottom moves
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
                // Restriction on moves from above
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

        /* Drawing Checkers
         */
        public override void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
            graph.DrawEllipse(new Pen(Color.Black, 3),
                Position.screenPosition.X + Position.drawableSize.Width / 5, Position.screenPosition.Y + Position.drawableSize.Height / 5,
                Position.drawableSize.Width - (Position.drawableSize.Width / 5 * 2), Position.drawableSize.Height - (Position.drawableSize.Height / 5 * 2));

            if (selected) // If the checker is selected
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                    Position.screenPosition.X, Position.screenPosition.Y,
                    Position.drawableSize.Width, Position.drawableSize.Height);
        }
    }
}
