using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon
{
    static class Program
    {
        public static FormMenu mainMenu; // Главная форма
        public static bool close = false; // Индикатор закрытия приложения


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainMenu = new FormMenu(true);
            Application.Run(mainMenu);
        }
    }
}
