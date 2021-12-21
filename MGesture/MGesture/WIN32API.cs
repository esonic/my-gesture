using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MGesture
{
    internal class Win32API
    {
        private Win32API() { }

        /// <summary>
        /// 回调函数代理
        /// </summary>
        //public delegate bool CallBack(IntPtr hwnd, int lParam);

        #region DLL导入

        //[DllImportAttribute("gdi32.dll")]
        //public static extern IntPtr CreateDC(
        //string lpszDriver, // 驱动名称
        //string lpszDevice, // 设备名称
        //string lpszOutput, // 无用，可以设定位"NULL"
        //IntPtr lpInitData // 任意打印机数据
        //);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        //[DllImport("user32.dll")]
        //public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys key);

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

        /// <summary>
        /// 调整鼠标移动速度
        /// </summary>
        /// <param name="speed"></param>
        public static void SetMouseSpeed(uint speed)
        {
            if (speed > 0 && speed < 21)
            {
                //SPI_SETMOUSESPEED   0x0071
                //SPIF_SENDCHANGE = 0×2
                SystemParametersInfo(0x0071, 0, speed, 0x2);
            }
        }

        //[DllImport("user32.dll")]
        //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        //[DllImport("user32.dll")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        //[DllImport("user32.dll")]
        //public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 检测按键是否正在按下中
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyDown(System.Windows.Forms.Keys key)
        {
            if ((GetAsyncKeyState(key) & 0x8000) == 0x8000)
            {
                //若按键正在按下则 GetAsyncKeyState 的返回值最高位为1
                return true;
            }
            else return false;
        }

        public static bool IsShiftDown()
        {
            if (IsKeyDown(System.Windows.Forms.Keys.LShiftKey) || IsKeyDown(System.Windows.Forms.Keys.RShiftKey))
                return true;
            else return false;
        }

        public static bool IsCtrlDown()
        {
            if (IsKeyDown(System.Windows.Forms.Keys.LControlKey) || IsKeyDown(System.Windows.Forms.Keys.RControlKey))
                return true;
            else return false;
        }

        public static bool IsAltDown()
        {
            if (IsKeyDown(System.Windows.Forms.Keys.LMenu) || IsKeyDown(System.Windows.Forms.Keys.RMenu))
                return true;
            else return false;
        }

        public static bool IsWinDown()
        {
            if (IsKeyDown(System.Windows.Forms.Keys.LWin) || IsKeyDown(System.Windows.Forms.Keys.RWin))
                return true;
            else return false;
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnstring, int uReturnLength, int hwndCallback);

        /// <summary>
        /// 控制光驱的开合
        /// </summary>
        /// <param name="cdVolume">要控制的光驱的盘符</param>
        /// <param name="isOpen">是开启还是关闭</param>
        public static void CDDoorControl(char cdVolume, bool isOpen)
        {
            if (isOpen)
            {
                mciSendString("open " + cdVolume.ToString() + ": type cdaudio alias cdaudio ", null, 0, 0);
                mciSendString("set cdaudio door open", null, 0, 0);
                mciSendString("close cdaudio", null, 0, 0);
            }
            else
            {
                mciSendString("open " + cdVolume.ToString() + ": type cdaudio alias cdaudio ", null, 0, 0);
                mciSendString("set cdaudio door closed", null, 0, 0);
                mciSendString("close cdaudio", null, 0, 0);
            }
        }

        [DllImport("user32.dll")] //winuser.h
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);

        //[DllImport("User32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);

        //用于恢复窗口显示的外部函数
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, WindowState cmdShow);

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount);

        //[DllImport("user32.dll")]
        //public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool IsZoomed(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        /// <summary>
        /// 设置窗口的位置和大小，若窗口为最大化或最小化则不起作用
        /// </summary>
        /// <param name="hwnd">目标窗口句柄</param>
        /// <param name="cx">宽度</param>
        /// <param name="cy">高度</param>
        /// <param name="center">是否将窗口居中放置，为false将不改变位置</param>
        public static void SetWindowSize(IntPtr hwnd, int cx, int cy, bool center)
        {
            if (!IsZoomed(hwnd) && !IsIconic(hwnd))
            {
                if (center)
                {
                    int x = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - cx) / 2;
                    int y = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - cy) / 2;
                    //NoZOrder = 0x4
                    SetWindowPos(hwnd, 0, x, y, cx, cy, 0x4);
                }
                else
                {
                    //NoMove = 0x2
                    SetWindowPos(hwnd, 0, 0, 0, cx, cy, 0x2 | 0x4);
                }
            }
        }

        //由窗口句柄得到进程ID
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int processId);

        [DllImport("User32.dll")]
        private static extern bool ExitWindowsEx(int uFlags, int dwReserved);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="type">退出参数</param>
        /// <returns>是否成功</returns>
        public static bool ExitWindows(ShutdownType type)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx((int)type, 0);

            return ok;
        }

        [DllImport("shell32.dll")]
        public static extern UInt32 SHAppBarMessage(UInt32 dwMessage, ref APPBARDATA pData);

        #endregion DLL导入

        //用于显示窗口的常量
        public enum WindowState { SW_SHOWNORMAL = 1, SW_SHOWMINIMIZED = 2, SW_SHOWMAXIMIZED = 3 }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public UInt32 cbSize;
            public IntPtr hWnd;
            public UInt32 uCallbackMessage;
            public UInt32 uEdge;
            public RECT rc;
            public Int32 lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        private const int SE_PRIVILEGE_ENABLED = 0x00000002;
        private const int TOKEN_QUERY = 0x00000008;
        private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        //用于关闭系统等
        public enum ShutdownType
        {
            LogOff = 0,
            PowerOff = 0x00000008,
            Reboot = 0x00000002
        }

        //[StructLayout(LayoutKind.Explicit, Size = 28)]
        //public struct INPUT
        //{
        //    [FieldOffset(0)]
        //    public uint type;
        //    [FieldOffset(4)]
        //    public KEYBDINPUT ki;
        //};

        //public struct KEYBDINPUT
        //{
        //    public ushort wVk;
        //    public ushort wScan;
        //    public uint dwFlags;
        //    public long time;
        //    public uint dwExtraInfo;
        //};
    }
}