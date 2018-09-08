using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Helpers
{
    /* Комбинированные координаты объекта
     */
    [Serializable]
    public class CheckersCoordinateSet
    {
        public Point screenPosition; // Позиция на экране
        public Size drawableSize; // Размер для отрисовки
        public Point boardPosition; // Позиция на игровой доске

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
