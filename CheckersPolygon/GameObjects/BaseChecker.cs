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
    /* The base class that defines the general behavior of the checker
     */
    [Serializable]
    class BaseChecker : IDrawable, IChecker
    {
        /* Determined by:
         * - Side (Black / White)
         * - Direction of travel (Up / Down / In both directions (king))
         * - Combined position (Position on the screen, size on the screen, position on the board)
         */
        public BaseChecker(CheckerSide side, CheckerMoveDirection direction, CheckersCoordinateSet position)
        {
            this.Side = side;
            this.Position = position;
            this.ZOrder = 1;
            this.Direction = direction;
        }

        public byte ZOrder { get; set; } // The rendering layer
        public CheckersCoordinateSet Position { get; set; } // Combined checker position
        public CheckerSide Side { get; set; } // Checker side (b / w)
        public CheckerMoveDirection Direction { get; set; } // The direction of the checker's move
        public byte TurnRange { get; set; } // Maximum distance of the move of the checker
        public bool selected; // Checker selected

        /* Destroying a checker, unsubscribing from a list for rendering
         */
        public void Destroy()
        {
            Game.drawingController.DeleteFromDrawingList(this);
        }

        /* Drawing Checkers
         */
        public virtual void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);

            if (selected) // Select a checker if it is selected
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
        }

        /* Moving the checker to the specified position
         */
        public void MoveTo(Point boardPosition)
        {
            this.Position.boardPosition = boardPosition;
            this.Position.drawableSize = Game.gameplayController.cellSize;
            this.Position.screenPosition.X = this.Position.drawableSize.Width * boardPosition.X;
            this.Position.screenPosition.Y = this.Position.drawableSize.Height * boardPosition.Y;
        }

        /* Position in the coordinates of the marks on the board (eq. C8, A2, H5, ...)
         */
        public string GetPrintablePosition()
        {
            return (String.Format("{1}{0}", 
                (char)('8' - Convert.ToByte(this.Position.boardPosition.Y)),
                (char)('A' + Convert.ToByte(this.Position.boardPosition.X))));
        }

        /* List of possible moves for the checker
         */
        public PathPoint GetPossibleTurns()
        {
            return GetPossibleTurns(null);
        }

        /* List of possible moves for a checker with a cut off diagonal
         */
        public virtual PathPoint GetPossibleTurns(TurnDirection? bannedDirection)
        {
            Pathfinder pathfinder = new Pathfinder();
            PathPoint turns = pathfinder.GetTurns(Position.boardPosition, this.TurnRange, Side, bannedDirection);

            return turns;
        }
    }
}
