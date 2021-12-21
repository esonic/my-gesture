using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyHook
{
    internal class Win32API
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(WH_Codes idHook, HookProc lpfn, IntPtr pInstance, int threadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int CallNextHookEx(IntPtr pHookHandle, int nCode, Int32 wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern IntPtr WindowFromPoint(POINT point);
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetAncestor(IntPtr hWnd, uint flag);
        //[DllImport("user32.dll")]
        //public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        //[DllImport("user32.dll")]
        //public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetDoubleClickTime();

        //由窗口句柄得到进程ID
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int processId);

        [DllImport("psapi.dll", CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //private static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount);

        //[DllImport("user32.dll", SetLastError = false)]
        //private static extern IntPtr GetDesktopWindow();

        //获取窗体大小的API
        [DllImport("user32")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        //RECT结构体定义
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private static System.Drawing.Rectangle screenRect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        /// <summary>
        /// 监测是否全屏应用程序
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool IsFullScreenApp(IntPtr hwnd)
        {
            RECT r;
            GetClientRect(hwnd, out r);
            if ((r.Right - r.Left) >= screenRect.Width && (r.Bottom - r.Top) >= screenRect.Height)
            {
                return true;
            }
            else return false;
        }
    }
}