using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CSharpInterpreter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length > 0)
                    Application.Run(new MainForm(args[0]));
                else Application.Run(new MainForm(null));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal Error:\r\n\r\n" + ex, "CSharp Interpreter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                catch { }
            }
        }
    }
}
