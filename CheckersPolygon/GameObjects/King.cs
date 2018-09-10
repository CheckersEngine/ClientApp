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
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
            // Crown of the King
            Point[] crown = new Point[]
                {
                    new Point(Position.screenPosition.X + Position.drawableSize.Width / 5, 
                    Position.screenPosition.Y + Position.drawableSize.Height / 5),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width / 5, 
                    Position.screenPosition.Y + Position.drawableSize.Height - Position.drawableSize.Height / 5),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width - Position.drawableSize.Width / 5,
                    Position.screenPosition.Y + Position.drawableSize.Height - Position.drawableSize.Height / 5),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width - Position.drawableSize.Width / 5,
                    Position.screenPosition.Y + Position.drawableSize.Height / 5),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width - Position.drawableSize.Width / 4,
                    Position.screenPosition.Y + Position.drawableSize.Height / 2 - Position.drawableSize.Height / 4),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width/2,
                    Position.screenPosition.Y + Position.drawableSize.Height / 5),
                    new Point(Position.screenPosition.X + Position.drawableSize.Width / 4,
                    Position.screenPosition.Y + Position.drawableSize.Height / 2 - Position.drawableSize.Height / 4),
                };
            graph.FillPolygon(Brushes.Black, crown);
            if (selected) // Highlighting of a king, if selected
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                    Position.screenPosition.X, Position.screenPosition.Y,
                    Position.drawableSize.Width, Position.drawableSize.Height);
        }
    }
}
