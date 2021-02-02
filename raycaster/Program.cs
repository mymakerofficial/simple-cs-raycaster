using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raycaster
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        public static GameWindow gameWindow;
        public static LevelEditor levelEditor;
        public static DebugWindow debugWindow;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Game.Create();
            levelEditor = new LevelEditor();
            gameWindow = new GameWindow();
            debugWindow = new DebugWindow();
            Game.Start();
            //debugWindow.Show();
            //levelEditor.Show();
            Application.Run(gameWindow);
        }
    }
}