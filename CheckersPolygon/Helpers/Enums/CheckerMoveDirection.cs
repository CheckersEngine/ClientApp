using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers.Enums
{
    /* Possible directions of the course
     */
    enum CheckerMoveDirection
    {
        Upstairs = 1, // Up
        Downstairs = -1, // Down
        Both = 2 // In both directions (King)
    }
}
