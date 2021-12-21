using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace MyHook
{
    //鼠标事件代理
    public delegate void MyMouseEventHandler(MouseEventFlags flag, int x, int y);
    public delegate void MyKeyEventHandler(Keys key, ref bool handled);

    /// <summary>
    /// 捕获鼠标左键和右键单击及移动事件
    /// </summary>
    public class MyHooks
    {
        #region 私有变量

        /// <summary>
        /// 鼠标钩子句柄
        /// </summary>
        private IntPtr m_pMouseHook = IntPtr.Zero;

        /// <summary>
        /// 键盘钩子句柄
        /// </summary>
        private IntPtr m_pKeyboardHook = IntPtr.Zero;

        /// <summary>
        /// 鼠标钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量,否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手 (MDA)。 
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private HookProc m_MouseHookProcedure;

        /// <summary>
        /// 键盘钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量,否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手 (MDA)。 
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private HookProc m_KeyboardHookProcedure;

        /// <summary>
        /// 鼠标事件。click为-1表示释放按键，0为鼠标移动
        /// </summary>
        public event MyMouseEventHandler OnMouseActivity;

        /// <summary>
        /// 按键释放事件 (KeyData)
        /// </summary>
        public event MyKeyEventHandler OnKeyUp;
        public event MyKeyEventHandler OnKeyDown;

        #endregion 私有变量

        #region 私有方法

        /// <summary>
        /// 鼠标钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (OnMouseActivity != null))
            {
                //detect button clicked
                //MouseButtons button = MouseButtons.Left;
                //int clickCount = 0; //move
                MouseEventFlags flag = 0;

                switch (wParam)
                {
                    case (int)WM_MOUSE.WM_RBUTTONDOWN:
                        //button = MouseButtons.Right;
                        //clickCount = 1;
                        flag = MouseEventFlags.RD;
                        break;
                    case (int)WM_MOUSE.WM_RBUTTONUP:
                        //button = MouseButtons.Right;
                        //clickCount = -1;
                        flag = MouseEventFlags.RU;
                        break;
                    case (int)WM_MOUSE.WM_MOUSEMOVE:
                        //button = MouseButtons.None;
                        flag = MouseEventFlags.MO;
                        break;
                    case (int)WM_MOUSE.WM_LBUTTONDOWN:
                        //clickCount = 1;
                        flag = MouseEventFlags.LD;
                        break;
                    case (int)WM_MOUSE.WM_LBUTTONUP:
                        //clickCount = -1;
                        flag = MouseEventFlags.LU;
                        break;
                    case (int)WM_MOUSE.WM_MBUTTONDOWN:
                        //button = MouseButtons.Middle;
                        //clickCount = 1;
                        flag = MouseEventFlags.MD;
                        break;
                    case (int)WM_MOUSE.WM_MBUTTONUP:
                        //button = MouseButtons.Middle;
                        //clickCount = -1;
                        flag = MouseEventFlags.MU;
                        break;
                    default:
                        return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                }

                    //moving
                MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                OnMouseActivity(flag, mouseHookStruct.Point.X, mouseHookStruct.Point.Y);
            }
            return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 键盘钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            bool handled = false;
            //it was ok and someone listens to events
            if (nCode >= 0)
            {
                //read structure KeyboardHookStruct at lParam
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                Keys keyData = (Keys)MyKeyboardHookStruct.VKCode;

                // raise KeyDown
                if (this.OnKeyDown != null && (wParam == (int)WM_KEYBOARD.WM_KEYDOWN || wParam == (int)WM_KEYBOARD.WM_SYSKEYDOWN))
                {
                    this.OnKeyDown(keyData, ref handled);
                }
                else if (this.OnKeyUp != null && (wParam == (int)WM_KEYBOARD.WM_KEYUP || wParam == (int)WM_KEYBOARD.WM_SYSKEYUP))
                {
                    this.OnKeyUp(keyData, ref handled);
                }
            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return 1;
            else
                return Win32API.CallNextHookEx(this.m_pKeyboardHook, nCode, wParam, lParam);
        }

        #endregion 私有方法

        #region 公共方法

        /// <summary>
        /// 安装鼠标钩子
        /// </summary>
        /// <returns></returns>
        public bool InstallMouseHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            // 假如没有安装鼠标钩子
            if (this.m_pMouseHook == IntPtr.Zero)
            {
                this.m_MouseHookProcedure = new HookProc(this.MouseHookProc);
                this.m_pMouseHook = Win32API.SetWindowsHookEx(WH_Codes.WH_MOUSE_LL, this.m_MouseHookProcedure, pInstance, 0);
                if (this.m_pMouseHook == IntPtr.Zero)
                {
                    this.UnInstallMouseHook();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 安装键盘钩子
        /// </summary>
        /// <returns></returns>
        public bool InstallKeyHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            //如果没有安装键盘钩子
            if (this.m_pKeyboardHook == IntPtr.Zero)
            {
                this.m_KeyboardHookProcedure = new HookProc(this.KeyboardHookProc);
                this.m_pKeyboardHook = Win32API.SetWindowsHookEx(WH_Codes.WH_KEYBOARD_LL, this.m_KeyboardHookProcedure, pInstance, 0);
                if (this.m_pKeyboardHook == IntPtr.Zero)
                {
                    this.UninstallKeyHook();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 卸载鼠标钩子
        /// </summary>
        /// <returns></returns>
        public bool UnInstallMouseHook()
        {
            bool result = true;
            if (this.m_pMouseHook != IntPtr.Zero)
            {
                result = (Win32API.UnhookWindowsHookEx(this.m_pMouseHook) && result);
                this.m_pMouseHook = IntPtr.Zero;
            }
            return result;
        }

        /// <summary>
        /// 卸载键盘钩子
        /// </summary>
        /// <returns></returns>
        public bool UninstallKeyHook()
        {
            bool result = true;

            if (this.m_pKeyboardHook != IntPtr.Zero)
            {
                result = (Win32API.UnhookWindowsHookEx(this.m_pKeyboardHook) && result);
                this.m_pKeyboardHook = IntPtr.Zero;
            }

            return result;
        }

        #endregion 公共方法

        #region 构造函数

        /// <summary>
        /// 全局键盘及鼠标钩子，使用时请响应相应的事件
        /// </summary>
        public MyHooks() { }

        #endregion 构造函数
    }
}