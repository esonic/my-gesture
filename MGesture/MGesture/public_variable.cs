using System;
using System.Runtime.InteropServices;

namespace MGesture
{
    #region 枚举定义

    public enum MouseEventFlags
    {
        LeftClick = 0x00000099,
        RightClick,
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        Move = 0x00000001,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    public enum KeyEventType { KeyDown, KeyUp, KeyPress }

    #endregion 枚举定义
}