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
    /* Game board marker
     */
    class BoardMarker : IDrawable
    {
        public char marker; // Marker mark
        public Color color; // Marker color
        public byte ZOrder { get; set; } // The rendering layer
        public CheckersCoordinateSet Position { get; set; } // Combined position

        /* Determined by:
         * - the symbol of the marker
         * - position
         */
        public BoardMarker(char marker, CheckersCoordinateSet position)
        {
            this.marker = marker;
            ZOrder = 2;
            this.Position = position;
            this.color = Constants.boardMarkerColor;
        }

        /* Drawing a marker
         */
        public void Draw(Graphics graph)
        {
            Font font = new Font("Century Gothic", 12f);
            Brush brush = new SolidBrush(color);
            graph.DrawString(marker.ToString(), font, brush, Position.screenPosition);
        }
    }
}
