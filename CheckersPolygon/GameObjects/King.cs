using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using CheckersPolygon.Controllers;
using System.Drawing;

namespace CheckersPolygon.GameObjects
{
    /* King, walks to any distance in any direction
     */
    [Serializable]
    class King : BaseChecker
    {
        public King(CheckerSide side, CheckersCoordinateSet position) : base(side, CheckerMoveDirection.Both, position)
        {
            this.TurnRange = 8; // 8-cell move
        }

        /* Drawing a king
         */
        public override void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.ScreenPosition.X, Position.ScreenPosition.Y,
                Position.DrawableSize.Width, Position.DrawableSize.Height);
            // Crown of the King
            Point[] crown = new Point[]
                {
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width / 5, 
                    Position.ScreenPosition.Y + Position.DrawableSize.Height / 5),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width / 5, 
                    Position.ScreenPosition.Y + Position.DrawableSize.Height - Position.DrawableSize.Height / 5),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width - Position.DrawableSize.Width / 5,
                    Position.ScreenPosition.Y + Position.DrawableSize.Height - Position.DrawableSize.Height / 5),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width - Position.DrawableSize.Width / 5,
                    Position.ScreenPosition.Y + Position.DrawableSize.Height / 5),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width - Position.DrawableSize.Width / 4,
                    Position.ScreenPosition.Y + Position.DrawableSize.Height / 2 - Position.DrawableSize.Height / 4),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width/2,
                    Position.ScreenPosition.Y + Position.DrawableSize.Height / 5),
                    new Point(Position.ScreenPosition.X + Position.DrawableSize.Width / 4,
                    Position.ScreenPosition.Y + Position.DrawableSize.Height / 2 - Position.DrawableSize.Height / 4),
                };
            graph.FillPolygon(Brushes.Black, crown);
            if (selected) // Highlighting of a king, if selected
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                    Position.ScreenPosition.X, Position.ScreenPosition.Y,
                    Position.DrawableSize.Width, Position.DrawableSize.Height);
        }
    }
}
