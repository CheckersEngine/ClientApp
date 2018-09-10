using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Interfaces
{
    /* Common interface of every checker entity
     */
    interface IChecker
    {
        // Checker side (black/white)
        CheckerSide Side { get; set; }

        // Turn direction (upward/downward/both directions)
        CheckerMoveDirection Direction { get; set; }
        
        // Maximum turn range
        byte TurnRange { get; set; }

        // -----------------------------------------------------------------------------------------------------
        // Getting list of all possible turns
        PathPoint GetPossibleTurns(TurnDirection? bannedDirection);

        // Movement
        void MoveTo(Point position);

        // Self-destroying
        void Destroy();
    }
}
