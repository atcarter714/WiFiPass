#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace WiFiPass
{
    internal static class Program
    {
        static MainForm mainForm = null;

        /// <summary>
        /// Gets the instance of the MainForm
        /// </summary>
        public static MainForm MainForm => mainForm;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] static void Main ( ) {

            Application.SetHighDpiMode( HighDpiMode.SystemAware );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( ( mainForm = new MainForm() ) );
        }
    }
}
