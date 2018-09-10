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
        public Point screenPosition; // Position on the screen
        public Size drawableSize; // Size for rendering
        public Point boardPosition; // Position on the game board

        public CheckersCoordinateSet()
        {
            this.screenPosition = new Point();
            this.drawableSize = new Size();
            this.boardPosition = new Point();
        }

        public CheckersCoordinateSet(Point boardPosition)
        {
            this.screenPosition = new Point();
            this.drawableSize = new Size();
            this.boardPosition = boardPosition;
        }

        public CheckersCoordinateSet(Rectangle screenCoordinates, Point boardPosition)
        {
            this.screenPosition = screenCoordinates.Location;
            this.drawableSize = screenCoordinates.Size;
            this.boardPosition = boardPosition;
        }

        public CheckersCoordinateSet(Point screenPosition, Size drawableSize, Point boardPosition)
        {
            this.screenPosition = screenPosition;
            this.drawableSize = drawableSize;
            this.boardPosition = boardPosition;
        }
    }
}
