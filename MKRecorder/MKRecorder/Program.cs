using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MKRecorder
{
    public static class Program
    {
        internal static MainForm mainFrm;
        //public static string volume;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
    
            ////////////////////////////////////////////////////////////////

            mainFrm = new MainForm();
            try
            {
                Application.Run(mainFrm);
            }
            catch(ObjectDisposedException) { }
        }
    }
}
