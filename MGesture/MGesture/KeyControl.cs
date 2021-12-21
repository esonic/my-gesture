using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MGesture
{
    class KeyControl
    {
        //private const ushort KEYEVENTF_KEYUP = 0x0002;
        private KeyControl() { }

        //public static void KeyEvent(KeyEventType type, Keys key)
        //{
        //    uint intReturn = 0;
        //    Win32API.INPUT structInput = new Win32API.INPUT();
        //    structInput.type = (uint)1;
        //    structInput.ki.wScan = 0;
        //    structInput.ki.time = 0;
        //    structInput.ki.wVk = (ushort)key;
        //    structInput.ki.dwExtraInfo = 0;

        //    if (type == KeyEventType.KeyDown)
        //    {
        //        structInput.ki.dwFlags = 0;
        //        intReturn = Win32API.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        //    }
        //    else if (type == KeyEventType.KeyPress)
        //    {
        //        structInput.ki.dwFlags = 0;
        //        intReturn = Win32API.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        //        structInput.ki.dwFlags = 0x0002;
        //        intReturn = Win32API.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        //    }
        //    else if (type == KeyEventType.KeyUp)
        //    {
        //        structInput.ki.dwFlags = 0x0002;
        //        intReturn = Win32API.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        //    }
        //    if (intReturn == 0)
        //    {
        //        MessageBox.Show("fail");
        //    }
        //}

        public static void KeyEvent(KeyEventType type, Keys key)
        {
            //const int KEYEVENTF_KEYUP = 0x2;
            //const int KEYEVENTF_EXTENDEDKEY = 0x1;
            uint scanCode = Win32API.MapVirtualKey((uint)key, 0);

            if (type == KeyEventType.KeyDown)
            {
                Win32API.keybd_event((byte)key, (byte)scanCode, 0x1, 0);
            }
            else if (type == KeyEventType.KeyPress)
            {
                Win32API.keybd_event((byte)key, (byte)scanCode, 0x1, 0);
                //System.Threading.Thread.Sleep(10);
                Win32API.keybd_event((byte)key, (byte)scanCode, 0x1 | 0x2, 0);
            }
            else if (type == KeyEventType.KeyUp)
            {
                Win32API.keybd_event((byte)key, (byte)scanCode, 0x1 | 0x2, 0);
            }
            //System.Threading.Thread.Sleep(10);
        }
    }
}