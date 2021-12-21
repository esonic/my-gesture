using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MGesture
{
    /// <summary>
    /// 启动一个进程
    /// </summary>
    class StartProAction : MyAction
    {
        private string fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        public StartProAction(string fileName)
            : base(ActionType.StartProgram)
        {
            this.fileName = fileName;
        }

        public override void execute()
        {
            //try
            //{
                string temp = fileName;
                if (fileName.Contains("{cp}"))
                {
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.C);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);

                    System.Threading.Thread.Sleep(50);
                    temp = fileName.Replace("{cp}", MainForm.GetClipText());
                }
                Process.Start(temp);
            //}
            //catch (Exception)
            //{
                //MessageBox.Show("启动进程失败");
            //}
        }
    }
}
