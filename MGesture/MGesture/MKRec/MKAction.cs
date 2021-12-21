using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MKRecorder
{
    /// <summary>
    /// 动作接口
    /// </summary>
    public abstract class IMKAction
    {
        //时间
        public int Time { get; set; }

        //模拟该动作执行
        public abstract void Execute();
    }

    /// <summary>
    /// 键盘动作
    /// </summary>
    public class KeyAction : IMKAction
    {
        private Keys key;
        private bool isUp;

        public Keys Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool IsUp
        {
            get { return isUp; }
            set { isUp = value; }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private static void KeyEvent(Keys key, bool isUp)
        {
            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_EXTENDEDKEY = 0x1;

            //MAPVK_VK_TO_VSC   0
            uint scanCode = MapVirtualKey((uint)key, 0);

            if (isUp)
                keybd_event((byte)key, (byte)scanCode, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            else
                keybd_event((byte)key, (byte)scanCode, KEYEVENTF_EXTENDEDKEY, 0);
        }

        public KeyAction(Keys key, bool isUp, int time)
        {
            Time = time;
            this.key = key;
            this.isUp = isUp;
        }

        public KeyAction(string str)
        {
            string[] param = str.Split(' ');
            Time = int.Parse(param[1]);
            isUp = param[2] == "U" ? true : false;
            key = (Keys)Enum.Parse(typeof(Keys), param[3]);
        }

        public override void Execute()
        {
            KeyEvent(key, isUp);
        }

        public override string ToString()
        {
            //K 1000 U Enter
            return "K " + Time + " " + (isUp ? "U" : "D") + " " + key;
        }
    }

    /// <summary>
    /// 鼠标动作
    /// </summary>
    public class MouseAction : IMKAction
    {
        private MouseEventFlags flag;
        private int x, y;

        public MouseEventFlags Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private static void MouseEvent(MouseEventFlags f, int X, int Y)
        {
            SetCursorPos(X, Y);
            if (f != MouseEventFlags.MO)
            {
                mouse_event((uint)f, (uint)X, (uint)Y, 0, 0);
            }
        }

        public MouseAction(MouseEventFlags f, int x, int y, int time)
        {
            this.flag = f;
            this.x = x;
            this.y = y;
            Time = time;
        }

        public MouseAction(string str)
        {
            string[] param = str.Split(' ');
            Time = int.Parse(param[1]);
            x = int.Parse(param[2]);
            y = int.Parse(param[3]);
            flag = (MouseEventFlags)Enum.Parse(typeof(MouseEventFlags), param[4]);
        }

        public override void Execute()
        {
            MouseEvent(flag, x, y);
        }

        public override string ToString()
        {
            //M 1000 12 23 MOVE
            return "M " + Time + " " + x + " " + y + " " + flag;
        }
    }

    public enum MouseEventFlags
    {
        MO = 0x0001,
        LD = 0x0002,
        LU = 0x0004,
        RD = 0x0008,
        RU = 0x0010,
        MD = 0x0020,
        MU = 0x0040
    }
}
