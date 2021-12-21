using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace MGesture
{
    static class Program
    {
        internal static MainForm mainForm;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (RunningInstance() == null)
                {
                    //没有找到实例，开始运行程序
                    mainForm = new MainForm();
                    Application.Run(mainForm);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n\r\n" + e.StackTrace, "MGesture Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历正在有相同名字运行的进程 
            foreach (Process process in processes)
            {
                //忽略现有的进程 
                if (process.Id != current.Id)
                {
                    //确保进程从EXE文件运行 
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回另一个进程实例 
                        return process;
                    }
                }
            }
            //没有其它的进程，返回Null 
            return null;
        }
    }
}