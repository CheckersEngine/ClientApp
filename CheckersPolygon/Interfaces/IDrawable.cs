using CheckersPolygon.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Interfaces
{
    /* Common interface of every object that needs to be drawn
     */
    interface IDrawable
    {
        // Index of the drawing layer (0 - drawn first, 1 - second, ... etc.)
        Byte ZOrder { get; set; }

        // Combined position of the object
        CheckersCoordinateSet Position { get; set; }

        // Object rendering
        void Draw(Graphics graph);
    }
}
