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
        public List<BaseChecker> whiteCheckers = new List<BaseChecker>(12); // White checkers
        public List<BaseChecker> blackCheckers = new List<BaseChecker>(12); // Black checkers
        public bool phase; // The game phase (determines who is on top, who is on bottom)
        public CheckerSide turn = CheckerSide.White; // Which player owns the right of the current turn
        public BaseChecker activeChecker = null; // Checker during the move
        public PathPoint activeCheckerPathPoint = null; // Map of the moves of the active checker
        public bool isAiControlled = false;
        public CheckerSide aiSide = CheckerSide.Black;
    }
}
