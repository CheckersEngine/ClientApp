using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    [Serializable]
    public class LocalizedStrings
    {
        public string textMenuContinue = "Continue";
        public string textMenuNewGame = "New game";
        public string textMenuSaveGame = "Save game";
        public string textMenuLoadGame = "Load game";
        public string textMenuExit = "Exit";
        public string textMenuTitle = "Checkers";
        public string textMainMenu = "Menu";
        public string textModePlayer = "Player";
        public string textModeAI = "AI";
        public string textModeCancel = "Cancel";
        public string textModeTitle = "2-nd player";

        public string logGameSaved = "Game saved";
        public string logErrorGameSaved = "Error saving game";
        public string logError = "Error";
        public string logGameLoaded = "Game loaded";
        public string logErrorGameLoaded = "Error loading game";
        public string logBlackWon = "Black won";
        public string logWhiteWon = "White won";
        public string logWhiteToilet = "White lose with a shame";
        public string logBlackToilet = "Black lose with a shame";
        public string logTurn = "Turn:";
        public string logMoveTo = "to";

        public string sideWhite = "White turn";
        public string sideBlack = "Black turn";

        public string regExWhite = "(?i)white";
        public string regExBlack = "(?i)black";
        public string regExLoaded = "(?i)loaded";
        public string regExSaved = "(?i)saved";
        public string regExWon = "(?i)won";
    }
}
