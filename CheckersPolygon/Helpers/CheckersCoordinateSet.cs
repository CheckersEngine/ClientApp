using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Helpers
{
    /* Combined coordinates of the object
     */
    [Serializable]
    public class CheckersCoordinateSet
    {
        // Position on the screen
        public Point ScreenPosition { get; set; }

        // Size for rendering
        public Size DrawableSize { get; set; }

        // Position on the game board
        public Point BoardPosition { get; set; } 

        public CheckersCoordinateSet()
        {
            this.ScreenPosition = new Point();
            this.DrawableSize = new Size();
            this.BoardPosition = new Point();
        }

        public CheckersCoordinateSet(Point boardPosition)
        {
            this.ScreenPosition = new Point();
            this.DrawableSize = new Size();
            this.BoardPosition = boardPosition;
        }

        public CheckersCoordinateSet(Rectangle screenCoordinates, Point boardPosition)
        {
            this.ScreenPosition = screenCoordinates.Location;
            this.DrawableSize = screenCoordinates.Size;
            this.BoardPosition = boardPosition;
        }

        public CheckersCoordinateSet(Point screenPosition, Size drawableSize, Point boardPosition)
        {
            this.ScreenPosition = screenPosition;
            this.DrawableSize = drawableSize;
            this.BoardPosition = boardPosition;
        }
    }
}
