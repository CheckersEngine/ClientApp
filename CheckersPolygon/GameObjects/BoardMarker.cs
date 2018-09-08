using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersPolygon.Helpers;
using System.Drawing;

namespace CheckersPolygon.GameObjects
{
    /* Маркер игровой доски
     */
    class BoardMarker : IDrawable
    {
        public char marker; // Метка маркера
        public Color color; // Цвет маркера
        public byte ZOrder { get; set; } // Слой отрисовки
        public CheckersCoordinateSet Position { get; set; } // Комбинированная позиция

        /* Определяется:
         * - символом маркера
         * - позицией
         */
        public BoardMarker(char marker, CheckersCoordinateSet position)
        {
            this.marker = marker;
            ZOrder = 2;
            this.Position = position;
            this.color = Constants.boardMarkerColor;
        }

        /* Отрисовка маркера
         */
        public void Draw(Graphics graph)
        {
            Font font = new Font("Century Gothic", 12f);
            Brush brush = new SolidBrush(color);
            graph.DrawString(marker.ToString(), font, brush, Position.screenPosition);
        }
    }
}
