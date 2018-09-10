using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon
{
    static class Program
    {
        public static FormMenu mainMenu; // Main form
        public static bool close = false; // Application close flag


        /// <summary>
        /// Application entry point.
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
