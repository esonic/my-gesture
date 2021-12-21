using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyHook
{
    internal class Win32API
    {
        #region DLL����

        /// <summary>
        /// ��װ����
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SetWindowsHookEx(WH_Codes idHook, HookProc lpfn,
            IntPtr pInstance, int threadId);

        /// <summary>
        /// ж�ع���
        /// </summary>
        /// <param name="idHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);

        /// <summary>
        /// ���ݹ���
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int CallNextHookEx(IntPtr pHookHandle, int nCode,
            Int32 wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern IntPtr WindowFromPoint(POINT point);

        //�ɴ��ھ���õ�����ID
        //[DllImport("User32.dll")]
        //public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int processId);

        //[DllImport("User32.dll")]
        //public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //private static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount);

        //[DllImport("user32.dll", SetLastError = false)]
        //private static extern IntPtr GetDesktopWindow();

        //��ȡ�����С��API
        //[DllImport("user32")]
        //private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        //RECT�ṹ�嶨��
        //[StructLayout(LayoutKind.Sequential)]
        //private struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;
        //}

        ///// <summary>
        ///// ����Ƿ�ȫ��Ӧ�ó���
        ///// </summary>
        ///// <param name="hwnd"></param>
        ///// <returns></returns>
        //public static bool IsFullScreenApp(IntPtr hwnd)
        //{
        //    System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        //    RECT r;
        //    GetWindowRect(hwnd, out r);
        //    if ((r.Right - r.Left) >= rect.Width && (r.Bottom - r.Top) >= rect.Height)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        #endregion DLL����
    }
}