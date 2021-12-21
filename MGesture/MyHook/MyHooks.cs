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
    public delegate void MyMouseEventHandler(string program, MouseButtons button, int click, int x, int y);
    public delegate void MyKeyEventHandler(string program, Keys key, ref bool handled);

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
        /// 最后一次右键按下时鼠标所在窗口的执行文件
        /// </summary>
        //private string exeFileName;
        //private StringBuilder exeFilePath = new StringBuilder(280);

        /// <summary>
        /// 全屏应用是否停止识别
        /// </summary>
        private bool isFullScreenStop = false;

        /// <summary>
        /// 全屏应用是否停止识别
        /// </summary>
        public bool IsFullScreenStopReco
        {
            get
            {
                return isFullScreenStop;
            }
            set
            {
                isFullScreenStop = value;
            }
        }

        /// <summary>
        /// 是否临时停止hook
        /// </summary>
        public bool IsStopMouseHook
        {
            get;
            set;
        }

        //private IntPtr hWndMouseIn;
        ///// <summary>
        ///// 最后一次鼠标按下时鼠标所在的窗口
        ///// </summary>
        //public IntPtr HWndMouseIn
        //{
        //    get { return hWndMouseIn; }
        //}

        /// <summary>
        /// 鼠标通用动作的例外
        /// </summary>
        private HashSet<string> mouseExceptions = new HashSet<string>();

        private HashSet<string> keyExceptions = new HashSet<string>();

        /// <summary>
        /// 鼠标例外列表
        /// </summary>
        public HashSet<string> MouseExceptions
        {
            get { return mouseExceptions; }
        }

        public HashSet<string> KeyExceptions
        {
            get { return keyExceptions; }
        }

        /// <summary>
        /// 添加鼠标通用例外
        /// </summary>
        /// <param name="program"></param>
        public void AddMouseException(string program)
        {
            //if (!mouseExceptions.Contains(program))
            mouseExceptions.Add(program);
        }

        public void AddKeyException(string program)
        {
            //if (!keyExceptions.Contains(program))
            keyExceptions.Add(program);
        }

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
        /// 鼠标事件。clickCount为-1表示释放按键，0为鼠标移动
        /// 右键单击无效，因此检测到移动不符合规则时需要帮忙重做鼠标的单击动作
        /// </summary>
        public event MyMouseEventHandler OnMouseActivity;

        /// <summary>
        /// 按键释放事件 (KeyData)
        /// </summary>
        public event MyKeyEventHandler OnKeyUp;
        public event MyKeyEventHandler OnKeyDown;

        //上一次点击是否发生在任务栏
        private bool isInTaskbar = false;

        //上一次鼠标左键抬起的时间
        private DateTime lastLUp = new DateTime();
        private Point lastLUpP = new Point(0, 0);
        private StringBuilder exeFilePath = new StringBuilder(280);

        #endregion 私有变量

        #region 私有方法

        private string GetExePath(IntPtr hwnd)
        {
            exeFilePath.Remove(0, exeFilePath.Length);
            //try
            //{
            //右键按下时分析当前窗口的 exe 文件路径
            int pId = 0;
            //hWndMouseIn = Win32API.WindowFromPoint(mouseHookStruct.Point);
            Win32API.GetWindowThreadProcessId(hwnd, out pId);
            //the below line has extremely performance issue in win8
            //exeFileName = Process.GetProcessById(pId).MainModule.FileName;
            IntPtr hProc = Win32API.OpenProcess(1040, false, pId);
            //if (hProc != IntPtr.Zero)
            //{
            Win32API.GetModuleFileNameEx(hProc, IntPtr.Zero, exeFilePath, exeFilePath.Capacity);
            //}
            Win32API.CloseHandle(hProc);
            //MessageBox.Show(pId.ToString() + " " + hProc + " " + exeFilePath.ToString());
            //}
            //catch
            //{
            //exeFileName = "Error";
            //}
            return exeFilePath.ToString();
        }

        /// <summary>
        /// 鼠标钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode < 0 || IsStopMouseHook || OnMouseActivity == null)
            {
                return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
            }

            //Marshall the data from callback.
            MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

            //detect button clicked
            MouseButtons button = MouseButtons.None;
            int clickCount = 0; //move
            switch (wParam)
            {
                case (int)WM_MOUSE.WM_RBUTTONDOWN:
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case (int)WM_MOUSE.WM_RBUTTONUP:
                    button = MouseButtons.Right;
                    clickCount = -1;
                    break;
                case (int)WM_MOUSE.WM_MOUSEMOVE:
                    //button = MouseButtons.None;
                    clickCount = -2;
                    break;
                case (int)WM_MOUSE.WM_MBUTTONDOWN:
                    button = MouseButtons.Middle;
                    clickCount = 1;
                    break;
                case (int)WM_MOUSE.WM_MBUTTONUP:
                    button = MouseButtons.Middle;
                    clickCount = -1;
                    break;
                case (int)WM_MOUSE.WM_LBUTTONUP:
                    if ((DateTime.Now - lastLUp).TotalMilliseconds <= Win32API.GetDoubleClickTime())
                    {
                        if (lastLUpP.X == mouseHookStruct.Point.X && lastLUpP.Y == mouseHookStruct.Point.Y)
                        {
                            //Double Click Check
                            button = MouseButtons.Left;
                            clickCount = 2;
                        }
                    }
                    lastLUp = DateTime.Now;
                    lastLUpP.X = mouseHookStruct.Point.X;
                    lastLUpP.Y = mouseHookStruct.Point.Y;
                    break;
            }

            //右键事件不被继续传递
            if (button != MouseButtons.None && clickCount != 0)
            {
                if (button == MouseButtons.Right && clickCount == 1 && !Screen.PrimaryScreen.WorkingArea.Contains(mouseHookStruct.Point.X, mouseHookStruct.Point.Y))
                {
                    //要在工作区内，排除任务栏
                    isInTaskbar = true;
                    return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                }
                if (button == MouseButtons.Right && clickCount == -1 && isInTaskbar)
                {
                    isInTaskbar = false;
                    return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                }

                IntPtr hwnd = Win32API.GetForegroundWindow();
                //string exefile = "N/A";
                //if (clickCount >= 1)
                //{
                string exefile = GetExePath(hwnd);
                //}

                if (button == MouseButtons.Middle)
                {
                    OnMouseActivity(exefile, button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y);
                    return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                }
                else if (button == MouseButtons.Left)
                {
                    //OnMouseActivity(exeFileName, button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y);
                    bool handled = false;
                    OnKeyDown(exefile, Keys.LButton, ref handled);
                    return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                }
                else
                {
                    if (MouseExceptions.Contains(exefile) || IsFullScreenAppStop(hwnd, exefile))
                    {
                        return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
                    }
                    else
                    {
                        OnMouseActivity(exefile, button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y);
                        return 1;
                    }
                }
            }
            else if (clickCount == -2)
            {
                //moving
                //MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                //generate event 
                //MouseEventArgs e = new MouseEventArgs(MouseButtons.None, 0, mouseHookStruct.Point.X, mouseHookStruct.Point.Y, 0);
                OnMouseActivity(null, MouseButtons.None, 0, mouseHookStruct.Point.X, mouseHookStruct.Point.Y);

                return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
            }
            else return Win32API.CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
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
            if (nCode < 0 || (this.OnKeyUp == null && this.OnKeyDown == null))
            {
                return Win32API.CallNextHookEx(this.m_pKeyboardHook, nCode, wParam, lParam);
            }

            //分析当前窗口的 exe 文件路径
            //string exeFileName = "";
            IntPtr hwnd = Win32API.GetForegroundWindow();
            string exefile = GetExePath(hwnd);
            if (KeyExceptions.Contains(exefile) || IsFullScreenAppStop(hwnd, exefile))
            {
                return Win32API.CallNextHookEx(this.m_pKeyboardHook, nCode, wParam, lParam);
            }

            //read structure KeyboardHookStruct at lParam
            KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

            // raise KeyDown
            if (this.OnKeyDown != null && (wParam == (int)WM_KEYBOARD.WM_KEYDOWN || wParam == (int)WM_KEYBOARD.WM_SYSKEYDOWN))
            {
                Keys keyData = (Keys)MyKeyboardHookStruct.VKCode;
                this.OnKeyDown(exefile, keyData, ref handled);
            }

            // raise KeyUp
            if (this.OnKeyUp != null && (wParam == (int)WM_KEYBOARD.WM_KEYUP || wParam == (int)WM_KEYBOARD.WM_SYSKEYUP))
            {
                Keys keyData = (Keys)MyKeyboardHookStruct.VKCode;
                this.OnKeyUp(exefile, keyData, ref handled);
            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return 1;
            else
                return Win32API.CallNextHookEx(this.m_pKeyboardHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 是否因全屏而停止识别
        /// </summary>
        /// <param name="exeFileName"></param>
        /// <returns></returns>
        private bool IsFullScreenAppStop(IntPtr hwnd, string exeFile)
        {
            if (isFullScreenStop)
            {
                //Explorer.EXE
                if (exeFile == null || hwnd == IntPtr.Zero || exeFile.ToLower().EndsWith("explorer.exe"))
                    return false;

                //开始检测
                if (Win32API.IsFullScreenApp(hwnd))
                    return true;
                else return false;
            }
            else return false;
        }

        #endregion 私有方法

        #region 公共方法

        /// <summary>
        /// 安装鼠标钩子
        /// </summary>
        /// <returns></returns>
        public int InstallMouseHook()
        {
            //IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            IntPtr pInstance = Win32API.GetModuleHandle("user32"); // after win8
            // 假如没有安装鼠标钩子
            if (this.m_pMouseHook == IntPtr.Zero)
            {
                this.m_MouseHookProcedure = new HookProc(this.MouseHookProc);
                this.m_pMouseHook = Win32API.SetWindowsHookEx(WH_Codes.WH_MOUSE_LL, this.m_MouseHookProcedure, pInstance, 0);
                if (this.m_pMouseHook == IntPtr.Zero)
                {
                    var err = Marshal.GetLastWin32Error();
                    this.UnInstallMouseHook();
                    return err;
                }
            }
            return 0;
        }

        /// <summary>
        /// 安装键盘钩子
        /// </summary>
        /// <returns></returns>
        public int InstallKeyHook()
        {
            //IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
            IntPtr pInstance = Win32API.GetModuleHandle("user32"); // after win8
            //如果没有安装键盘钩子
            if (this.m_pKeyboardHook == IntPtr.Zero)
            {
                this.m_KeyboardHookProcedure = new HookProc(this.KeyboardHookProc);
                this.m_pKeyboardHook = Win32API.SetWindowsHookEx(WH_Codes.WH_KEYBOARD_LL, this.m_KeyboardHookProcedure, pInstance, 0);
                if (this.m_pKeyboardHook == IntPtr.Zero)
                {
                    var err = Marshal.GetLastWin32Error();
                    this.UninstallKeyHook();
                    return err;
                }
            }
            return 0;
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
                result = Win32API.UnhookWindowsHookEx(this.m_pMouseHook);
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
                result = Win32API.UnhookWindowsHookEx(this.m_pKeyboardHook);
                this.m_pKeyboardHook = IntPtr.Zero;
            }

            return result;
        }

        #endregion 公共方法

        #region 构造函数

        /// <summary>
        /// 全局键盘及鼠标钩子，使用时请响应相应的事件
        /// </summary>
        public MyHooks() {
            IsStopMouseHook = false;
        }

        #endregion 构造函数
    }
}
