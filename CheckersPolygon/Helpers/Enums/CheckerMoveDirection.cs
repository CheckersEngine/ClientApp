using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers.Enums
{
    /* Возможные направления хода
     */
    enum CheckerMoveDirection
    {
        Upstairs = 1, // Вверх
        Downstairs = -1, // Вниз
        Both = 2 // В обе стороны (Дамка)
    }
}
