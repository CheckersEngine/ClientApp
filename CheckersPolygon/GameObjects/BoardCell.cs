using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CheckersPolygon.Helpers;

namespace CheckersPolygon.GameObjects
{
    /* Клетка доски
     */
    public class BoardCell : IDrawable
    {
        public bool IsActive { get; set; } // Если активная - черная, нет - белая
        public byte ZOrder { get; set; } // Слой отрисовки
        public CheckersCoordinateSet Position { get; set; } // Комбинированные координаты
        public Color color; // Цвет клетки
        public bool Highlighted { get; set; } // Подсвечена ли клетка

        /* Определяется:
         * - Флагом активности
         * - Комбинированной позицией
         */
        public BoardCell(bool isActive, CheckersCoordinateSet position)
        {
            this.IsActive = isActive;
            if (IsActive) color = Constants.activeCellColor;
            else color = Constants.passiveCellColor;
            this.Highlighted = false;
            this.Position = position;
            this.ZOrder = 0;
        }

        /* Отрисовка клетки
         */
        public void Draw(Graphics graph)
        {
            graph.FillRectangle(new SolidBrush(color),
                Position.screenPosition.X,
                Position.screenPosition.Y,
                Position.drawableSize.Width,
                Position.drawableSize.Height);

            if (Highlighted) // Если подсвечена
                graph.DrawRectangle(new Pen(Constants.highlightCellColor, 4),
                    Position.screenPosition.X,
                    Position.screenPosition.Y,
                    Position.drawableSize.Width,
                    Position.drawableSize.Height);
        }
    }
}
