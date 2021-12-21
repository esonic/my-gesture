using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MGesture
{
    class KeyAction : MyAction
    {
        /// <summary>
        /// 按键事件时使用
        /// </summary>
        private Keys key;

        public Keys Key
        {
            get { return key; }
        }
        /// <summary>
        /// 按键事件时的修饰符
        /// </summary>
        private bool shift = false;

        public bool Shift
        {
            get { return shift; }
        }
        private bool ctrl = false;

        public bool Ctrl
        {
            get { return ctrl; }
        }
        private bool alt = false;

        public bool Alt
        {
            get { return alt; }
        }
        private bool winKey = false;

        public bool WinKey
        {
            get { return winKey; }
        }

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="shift"></param>
        /// <param name="ctrl"></param>
        /// <param name="alt"></param>
        /// <param name="winKey"></param>
        public KeyAction(Keys key, bool shift, bool ctrl, bool alt, bool winKey) : base(ActionType.KeyPress)
        {
            this.key = key;
            this.shift = shift;
            this.ctrl = ctrl;
            this.alt = alt;
            this.winKey = winKey;
        }

        public override void execute()
        {
            if (shift)
                KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ShiftKey);
            if (ctrl)
                KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
            if (alt)
                KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
            if (winKey)
                KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.LWin);
            KeyControl.KeyEvent(KeyEventType.KeyPress, key);
            if (shift)
                KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ShiftKey);
            if (ctrl)
                KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
            if (alt)
                KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
            if (winKey)
                KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.LWin);
        }
    }
}
