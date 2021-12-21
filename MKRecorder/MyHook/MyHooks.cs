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
    //����¼�����
    public delegate void MyMouseEventHandler(MouseEventFlags flag, int x, int y);
    public delegate void MyKeyEventHandler(Keys key, ref bool handled);

    /// <summary>
    /// �������������Ҽ��������ƶ��¼�
    /// </summary>
    public class MyHooks
    {
        #region ˽�б���

        /// <summary>
        /// ��깳�Ӿ��
        /// </summary>
        private IntPtr m_pMouseHook = IntPtr.Zero;

        /// <summary>
        /// ���̹��Ӿ��
        /// </summary>
        private IntPtr m_pKeyboardHook = IntPtr.Zero;

        /// <summary>
        /// ��깳��ί��ʵ��
        /// </summary>
        /// <remarks>
        /// ��Ҫ��ͼʡ�Դ˱���,���򽫻ᵼ��
        /// ���� CallbackOnCollectedDelegate �йܵ������� (MDA)�� 
        /// ��ϸ��μ�MSDN�й��� CallbackOnCollectedDelegate ������
        /// </remarks>
        private HookProc m_MouseHookProcedure;

        /// <summary>
        /// ���̹���ί��ʵ��
        /// </summary>
        /// <remarks>
        /// ��Ҫ��ͼʡ�Դ˱���,���򽫻ᵼ��
        /// ���� CallbackOnCollectedDelegate �йܵ������� (MDA)�� 
        /// ��ϸ��μ�MSDN�й��� CallbackOnCollectedDelegate ������
        /// </remarks>
        private HookProc m_KeyboardHookProcedure;

        /// <summary>
        /// ����¼���clickΪ-1��ʾ�ͷŰ�����0Ϊ����ƶ�
        /// </summary>
        public event MyMouseEventHandler OnMouseActivity;

        /// <summary>
        /// �����ͷ��¼� (KeyData)
        /// </summary>
        public event MyKeyEventHandler OnKeyUp;
        public event MyKeyEventHandler OnKeyDown;

        #endregion ˽�б���

        #region ˽�з���

        /// <summary>
        /// ��깳�Ӵ�����
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
        /// ���̹��Ӵ�����
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

        #endregion ˽�з���

        #region ��������

        /// <summary>
        /// ��װ��깳��
        /// </summary>
        /// <returns></returns>
        public bool InstallMouseHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            // ����û�а�װ��깳��
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
        /// ��װ���̹���
        /// </summary>
        /// <returns></returns>
        public bool InstallKeyHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            //���û�а�װ���̹���
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
        /// ж����깳��
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
        /// ж�ؼ��̹���
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

        #endregion ��������

        #region ���캯��

        /// <summary>
        /// ȫ�ּ��̼���깳�ӣ�ʹ��ʱ����Ӧ��Ӧ���¼�
        /// </summary>
        public MyHooks() { }

        #endregion ���캯��
    }
}