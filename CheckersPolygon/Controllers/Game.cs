using CheckersPolygon.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* The main class of the game
     * Contains all controllers
     */
    static class Game
    {
        public static DrawingController drawingController; // Game scene rendering controller
        public static GameplayController gameplayController; // Game Processor
        public static UserLogController userLogController; // Information Panel Controller

        /* Starting the engine should always be performed before any other actions with the engine
         */
        public static void InitEngine(ref Panel gameBoard, ref RichTextBox userLog, bool phase, bool aiAffected)
        {
            drawingController = new DrawingController(ref gameBoard);
            gameplayController = new GameplayController(ref gameBoard, phase, aiAffected);
            userLogController = new UserLogController(ref userLog);
        }

        /* Saving the state of the game
         */
        public static void Save(string filename)
        {
            try
            {
                gameplayController.SaveState(filename);
                userLogController.WriteMessage(Constants.localized.logGameSaved);
            }
            catch (IOException ex)
            {
                MessageBox.Show(Constants.localized.logErrorGameSaved + ex.Message, Constants.localized.logError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Loading the game state
         */
        public static void Load(string filename)
        {
            try
            {
                gameplayController.LoadState(filename);
                userLogController.WriteMessage(Constants.localized.logGameLoaded);
            }
            catch (IOException ex)
            {
                MessageBox.Show(Constants.localized.logErrorGameLoaded + ex.Message, Constants.localized.logError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
