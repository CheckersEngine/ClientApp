using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CheckersPolygon.Helpers;

namespace CheckersPolygon.Controllers
{
    /* Game Status
     * Keeps in itself all the dynamic game objects
     */
    [Serializable]
    class GameState
    {
        // List of all checkers on game board
        public List<BaseChecker> AllCheckers { get; set; } = new List<BaseChecker>(24);

        // List contains only white checkers
        public List<BaseChecker> WhiteCheckers { get => AllCheckers.Where(u => u.Side == CheckerSide.White).ToList(); }

        // List contains only black checkers
        public List<BaseChecker> BlackCheckers { get => AllCheckers.Where(u => u.Side == CheckerSide.Black).ToList(); }

        // The game phase (determines who is on top, who is on bottom)
        public bool Phase { get; set; }

        // Which player owns the right of the current turn
        public CheckerSide TurnOwner { get; set; } = CheckerSide.White;

        // Checker during the move
        public BaseChecker ActiveChecker { get; set; } = null;

        // Map of the moves of the active checker
        public PathPoint ActiveCheckerPathPoint { get; set; } = null;

        // Is second player is computer
        public bool IsAiControlled { get; set; } = false;

        // Side of computer player
        public CheckerSide AiSide { get; set; } = CheckerSide.Black;
    }
}
