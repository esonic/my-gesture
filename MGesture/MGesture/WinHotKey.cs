using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MGesture
{
    //注册全局热键，拥有热键的窗口需要重载WndProc
    class WinHotKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, System.Windows.Forms.Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        private static bool key_Ctrl = false;
        private static bool key_Shift = false;
        private static bool key_Alt = false;
        private static bool key_Windows = false;
        private static Keys key_other;

        public static bool SetHotKey(bool bCtrl, bool bShift, bool bAlt, bool bWindows, Keys nowKey, IntPtr window, int id)
        {
            try
            {
                key_Alt = bAlt;
                key_Ctrl = bCtrl;
                key_Shift = bShift;
                key_Windows = bWindows;
                key_other = nowKey;

                KeyModifiers modifier = KeyModifiers.None;

                if (key_Ctrl)
                    modifier |= KeyModifiers.Control;
                if (key_Alt)
                    modifier |= KeyModifiers.Alt;
                if (key_Shift)
                    modifier |= KeyModifiers.Shift;
                if (key_Windows)
                    modifier |= KeyModifiers.Windows;

                return RegisterHotKey(window, id, modifier, nowKey);
            }
            catch
            {
                //login.ShowMessage("快捷键定义错误！"); 
                return false;
            }
        }

        public static void RemoveHotKey(IntPtr window, int id)
        {
            UnregisterHotKey(window, id);
        }
    }
}
