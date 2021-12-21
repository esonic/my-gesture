using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    /// <summary>
    /// 按键事件类型
    /// </summary>
    public enum K
    {
        /// <summary>
        /// 按下
        /// </summary>
        D,
        /// <summary>
        /// 抬起
        /// </summary>
        U,
        /// <summary>
        /// 点击
        /// </summary>
        P
    }

    /// <summary>
    /// 鼠标事件类型
    /// </summary>
    public enum M
    {
        /// <summary>
        /// 移动
        /// </summary>
        MO = 0x0001,
        /// <summary>
        /// 左键按下
        /// </summary>
        LD = 0x0002,
        /// <summary>
        /// 左键抬起
        /// </summary>
        LU = 0x0004,
        /// <summary>
        /// 右键按下
        /// </summary>
        RD = 0x0008,
        /// <summary>
        /// 右键抬起
        /// </summary>
        RU = 0x0010,
        /// <summary>
        /// 中键按下
        /// </summary>
        MD = 0x0020,
        /// <summary>
        /// 中键抬起
        /// </summary>
        MU = 0x0040
    }

    /// <summary>
    /// 系统退出参数
    /// </summary>
    public enum ExitType
    {
        /// <summary>
        /// 注销
        /// </summary>
        LogOff = 0,
        /// <summary>
        /// 关机
        /// </summary>
        PowerOff = 0x00000008,
        /// <summary>
        /// 重启
        /// </summary>
        Reboot = 0x00000002
    }

    /// <summary>
    /// 常用动作
    /// </summary>
    public class Actions
    {
        //private static ConsoleW console;

        #region import dll

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys key);

        [DllImport("User32.dll")]
        private static extern bool ExitWindowsEx(int uFlags, int dwReserved);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        #endregion

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="key">按键</param>
        public static void Key(K type, System.Windows.Forms.Keys key)
        {
            //const int KEYEVENTF_KEYUP = 0x2;
            //const int KEYEVENTF_EXTENDEDKEY = 0x1;
            uint scanCode = MapVirtualKey((uint)key, 0);

            if (type == K.D)
            {
                keybd_event((byte)key, (byte)scanCode, 0x1, 0);
            }
            else if (type == K.P)
            {
                keybd_event((byte)key, (byte)scanCode, 0x1, 0);
                //System.Threading.Thread.Sleep(10);
                keybd_event((byte)key, (byte)scanCode, 0x1 | 0x2, 0);
            }
            else if (type == K.U)
            {
                keybd_event((byte)key, (byte)scanCode, 0x1 | 0x2, 0);
            }
            //System.Threading.Thread.Sleep(10);
        }

        /// <summary>
        /// 鼠标事件
        /// </summary>
        /// <param name="f">事件类型</param>
        /// <param name="X">x坐标</param>
        /// <param name="Y">y坐标</param>
        public static void Mouse(M f, int X, int Y)
        {
            SetCursorPos(X, Y);
            if (f != M.MO)
            {
                mouse_event((uint)f, (uint)X, (uint)Y, 0, 0);
            }
        }

        /// <summary>
        /// 模拟按键序列
        /// </summary>
        /// <param name="keyStr">按键序列，仅支持可见字符</param>
        public static void Keys(string keyStr)
        {
            char[] keyArr = keyStr.ToCharArray();
            foreach (char ch in keyArr)
            {
                CharConverter.ProcessChar(ch);
            }
        }

        /// <summary>
        /// 获取剪贴板文本
        /// </summary>
        public static string ClipText
        {
            get
            {
                string text = "";
                System.Threading.Thread staThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    try
                    {
                        text = System.Windows.Forms.Clipboard.GetText();
                    }
                    catch { }
                }));
                staThread.SetApartmentState(System.Threading.ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                return text;
            }
        }

        /// <summary>
        /// 获取剪贴板文件路径集合
        /// </summary>
        public static System.Collections.Specialized.StringCollection ClipFiles
        {
            get
            {
                //string text = "";
                System.Collections.Specialized.StringCollection files = null;
                System.Threading.Thread staThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    try
                    {
                        files = System.Windows.Forms.Clipboard.GetFileDropList();
                        //foreach (string file in files)
                        //text += file + "|";
                    }
                    catch { }
                }));
                staThread.SetApartmentState(System.Threading.ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                return files;
            }
        }

        /// <summary>
        /// 是否某键按下
        /// </summary>
        /// <param name="key">按键</param>
        /// <returns>是否按下</returns>
        public static bool IsKeyDown(System.Windows.Forms.Keys key)
        {
            if ((GetAsyncKeyState(key) & 0x8000) == 0x8000)
            {
                //若按键正在按下则 GetAsyncKeyState 的返回值最高位为1
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="type">退出参数</param>
        /// <returns>是否成功</returns>
        public static bool ExitWindows(ExitType type)
        {
            //private const int SE_PRIVILEGE_ENABLED = 0x00000002;
            //private const int TOKEN_QUERY = 0x00000008;
            //private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
            //private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, 0x00000020 | 0x00000008, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = 0x00000002;
            ok = LookupPrivilegeValue(null, "SeShutdownPrivilege", ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx((int)type, 0);

            return ok;
        }

        /// <summary>
        /// 在对话框中执行方法, 将在窗口关闭后返回, eg.
        /// Actions.ExecuteInW((c) => {
        ///     c.WriteLine("test");
        /// mai});
        /// </summary>
        public static void ExecuteInW(ConsoleHandler e)
        {
            ConsoleW c = new ConsoleW();
            c.Load += (o, ea) =>
            {
                try
                {
                    if (e != null)
                        e(o as ConsoleW);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(c, "Exception: " + ex, "CSI - ExecuteInW failed", 
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            };
            System.Windows.Forms.Application.Run(c);
        }

        ///// <summary>
        ///// 启动一个线程
        ///// </summary>
        ///// <param name="m">线程执行方法</param>
        ///// <returns>启动的线程</returns>
        //public static System.Threading.Thread StartThread(System.Threading.ThreadStart m)
        //{
        //    if (m != null)
        //    {
        //        System.Threading.Thread t = new System.Threading.Thread(m);
        //        t.Start();
        //        return t;
        //    }
        //    else return null;
        //}

        ///// <summary>
        ///// 启动一个线程，附加执行参数
        ///// </summary>
        ///// <param name="m">线程执行方法</param>
        ///// <param name="arg">参数</param>
        ///// <returns>启动的线程</returns>
        //public static System.Threading.Thread StartThread(System.Threading.ParameterizedThreadStart m, object arg)
        //{
        //    if (m != null)
        //    {
        //        System.Threading.Thread t = new System.Threading.Thread(m);
        //        t.Start(arg);
        //        return t;
        //    }
        //    else return null;
        //}

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }

    /// <summary>
    /// 对话框执行代理
    /// </summary>
    /// <param name="c"></param>
    public delegate void ConsoleHandler(ConsoleW c);

    #region Console Window
    /// <summary>
    /// 控制台窗口
    /// </summary>
    public class ConsoleW : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtConsole = new System.Windows.Forms.TextBox();
        /// <summary>
        /// 创建窗口
        /// </summary>
        public ConsoleW()
        {
            Text = "Console Window";
            Size = new System.Drawing.Size(383, 227);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            txtConsole.ReadOnly = true;
            txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtConsole.Multiline = true;
            txtConsole.Location = new System.Drawing.Point(12, 12);
            txtConsole.Size = new System.Drawing.Size(343, 165);
            Controls.Add(txtConsole);
        }

        /// <summary>
        /// 写入字符串（线程安全）
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            try
            {
                Invoke(new EventHandler((o, e) =>
                {
                    txtConsole.AppendText(value);
                    txtConsole.SelectionStart = txtConsole.TextLength;
                    txtConsole.ScrollToCaret();
                }));
            }
            catch { }
        }

        /// <summary>
        /// 写入一行字符串（线程安全）
        /// </summary>
        /// <param name="line"></param>
        public void WriteLine(string line)
        {
            try
            {
                Invoke(new EventHandler((o, e) =>
                {
                    Write(line + "\r\n");
                }));
            }
            catch { }
        }
    }
    #endregion
}
