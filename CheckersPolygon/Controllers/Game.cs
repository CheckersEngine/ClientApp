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
    /* Главный класс игры
     * Содержит все контроллеры
     */
    static class Game
    {
        public static DrawingController drawingController; // Контроллер отрисовки игровой сцены
        public static GameplayController gameplayController; // Контроллер игрового процесса
        public static UserLogController userLogController; // Контроллер панели информирования

        /* Запуск движка, должен всегда выполняться перед какими- либо другими действиями с движком
         */
        public static void InitEngine(ref Panel gameBoard, ref RichTextBox userLog, bool phase, bool aiAffected)
        {
            drawingController = new DrawingController(ref gameBoard);
            gameplayController = new GameplayController(ref gameBoard, phase, aiAffected);
            userLogController = new UserLogController(ref userLog);
        }

        /* Сохранение состояния игры
         */
        public static void Save(string filename)
        {
            try
            {
                gameplayController.SaveState(filename);
                userLogController.WriteMessage("Игра сохранена");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Запись не удалась.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Загрузка состояния игры
         */
        public static void Load(string filename)
        {
            try
            {
                gameplayController.LoadState(filename);
                userLogController.WriteMessage("Игра загружена");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Загрузка не удалась.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
