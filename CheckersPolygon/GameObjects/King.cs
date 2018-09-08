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
    /* Дамка, ходит на любые расстояния в любую сторону
     */
    [Serializable]
    class King : BaseChecker
    {
        public King(CheckerSide side, CheckersCoordinateSet position) : base(side, CheckerMoveDirection.Both, position)
        {
            this.TurnRange = 8; // Ход на 8 клеток
        }

        /* Отрисовка дамки
         */
        public override void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
            // Корона дамки
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
            if (selected) // Выделение дамки, если выбрана
                graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                    Position.screenPosition.X, Position.screenPosition.Y,
                    Position.drawableSize.Width, Position.drawableSize.Height);
        }
    }
}
