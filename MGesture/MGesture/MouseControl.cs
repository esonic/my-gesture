using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

namespace MGesture
{
    class MouseControl
    {
        private MouseControl() { }

        public static void MouseEvent(MouseEventFlags f, int X, int Y)
        {
            if (X < 0 || Y < 0)
                return;
            //使用点击事件时X, Y没有作用，所以必须先移动鼠标到目标点

            if (f == MouseEventFlags.RightClick)
            {
                Win32API.mouse_event((uint)MouseEventFlags.RightDown, (uint)X, (uint)Y, 0, 0);
                //Thread.Sleep(10);
                Win32API.mouse_event((uint)MouseEventFlags.RightUp, (uint)X, (uint)Y, 0, 0);
            }
            else if (f == MouseEventFlags.LeftClick)
            {
                Win32API.mouse_event((uint)MouseEventFlags.LeftDown, (uint)X, (uint)Y, 0, 0);
                //Thread.Sleep(10);
                Win32API.mouse_event((uint)MouseEventFlags.LeftUp, (uint)X, (uint)Y, 0, 0);
            }
            else if (f == MouseEventFlags.Move)
            {
                Win32API.SetCursorPos(X, Y);
            }
            else Win32API.mouse_event((uint)f, (uint)X, (uint)Y, 0, 0);
        }
    }
}
