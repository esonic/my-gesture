using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace MGesture
{
    /// <summary>
    /// 动作的种类
    /// </summary>
    public enum ActionType
    {
        无, 前进, 后退, 最大化, 最小化, 刷新, 资源管理器, 上一窗口, 下一窗口, 关闭, 左键按下, 左键抬起, 右键按下, 右键抬起,
        静音, 音量减小, 音量增大, 下一首, 上一首, 播放暂停, 播放停止, KeyPress, 复制, 粘贴, 剪切,
        重做, 撤销, 关机, 重启, 注销, 显示桌面, 锁定并关闭屏幕, 重设窗口大小, 窗口总在最前, 自动隐藏任务栏, /*打开光驱, 关闭光驱,*/ 鼠标速度切换, StartProgram, MK, CS
    }

    /// <summary>
    /// 要执行的动作
    /// </summary>
    public class MyAction
    {
        /// <summary>
        /// 类型
        /// </summary>
        protected ActionType actionType;

        public ActionType ActionType
        {
            get { return actionType; }
            //set { actionType = value; }
        }

        /// <summary>
        /// 构造动作
        /// </summary>
        /// <param name="actionType"></param>
        public MyAction(ActionType actionType)
        {
            this.actionType = actionType;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public virtual void execute()
        {
            switch (actionType)
            {
                case ActionType.后退:
                    //后退 Alt + Left
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Left);
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Left);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
                    break;
                case ActionType.关闭:
                    //Alt + F4
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.F4);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
                    break;
                case ActionType.前进:
                    //前进
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Right);

                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Right);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
                    break;
                case ActionType.上一窗口:
                    //向左切换窗口 Alt + Esc
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Escape);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
                    break;
                case ActionType.最大化:
                    MaxOrMin(true);
                    break;
                case ActionType.最小化:
                    MaxOrMin(false);
                    break;
                case ActionType.静音:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeMute);
                    break;
                case ActionType.音量减小:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeDown);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeDown);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeDown);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeDown);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeDown);
                    break;
                case ActionType.音量增大:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeUp);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeUp);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeUp);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeUp);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.VolumeUp);
                    break;
                case ActionType.无:
                    break;
                case ActionType.资源管理器:
                    //打开一个新的资源浏览器窗口
                    Process.Start("Explorer.exe", ",");
                    break;
                case ActionType.刷新:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.F5);
                    break;
                case ActionType.下一窗口:
                    //向右切换窗口 Alt + Shift + Esc
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.Menu);
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ShiftKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Escape);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ShiftKey);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.Menu);
                    break;
                case ActionType.下一首:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.MediaNextTrack);
                    break;
                case ActionType.播放暂停:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.MediaPlayPause);
                    break;
                case ActionType.上一首:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.MediaPreviousTrack);
                    break;
                case ActionType.播放停止:
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.MediaStop);
                    break;
                case ActionType.复制:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.C);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
                    break;
                case ActionType.剪切:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.X);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
                    break;
                case ActionType.粘贴:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.V);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
                    break;
                case ActionType.重做:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Z);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
                    break;
                case ActionType.撤销:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.ControlKey);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Y);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.ControlKey);
                    break;
                case ActionType.重启:
                    Win32API.ExitWindows(Win32API.ShutdownType.Reboot);
                    break;
                case ActionType.注销:
                    Win32API.ExitWindows(Win32API.ShutdownType.LogOff);
                    break;
                case ActionType.关机:
                    Win32API.ExitWindows(Win32API.ShutdownType.PowerOff);
                    break;
                case ActionType.锁定并关闭屏幕:
                    //private const uint WM_SYSCOMMAND = 0x0112;
                    //private const uint SC_MONITORPOWER = 0xF170;
                    Win32API.LockWorkStation();
                    System.Threading.Thread.Sleep(500);
                    Win32API.SendMessage(Program.mainForm.Handle, 0x0112, 0xF170, 2);
                    break;
                case ActionType.显示桌面:
                    KeyControl.KeyEvent(KeyEventType.KeyDown, Keys.LWin);
                    KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.D);
                    KeyControl.KeyEvent(KeyEventType.KeyUp, Keys.LWin);
                    break;
                //case ActionType.性能窗口:
                //    ShowStat();
                //    break;
                //case ActionType.IE2CF:
                    //IECF();
                    //break;
                case ActionType.窗口总在最前:
                    IntPtr hwnd3 = Win32API.GetForegroundWindow();
                    if (hwnd3 != IntPtr.Zero)
                    {
                        //int HWND_TOPMOST = -1;
                        //int HWND_NOTOPMOST = -2;
                        //int WS_EX_TOPMOST = 0x00000008;
                        //int GWL_EXSTYLE = -20;
                        if ((Win32API.GetWindowLong(hwnd3, -20) & 0x00000008) == 0x00000008)
                        {
                            Win32API.SetWindowPos(hwnd3, -2, 0, 0, 0, 0, 0x1 | 0x2);
                        }
                        else Win32API.SetWindowPos(hwnd3, -1, 0, 0, 0, 0, 0x1 | 0x2);
                    }
                    break;
                case ActionType.重设窗口大小:
                    IntPtr hwnd2 = Win32API.GetForegroundWindow();
                    if (hwnd2 != IntPtr.Zero)
                    {
                        Win32API.SetWindowSize(hwnd2, Program.mainForm.IdealWindowSizeCx, Program.mainForm.IdealWindowSizeCy, true);
                    }
                    break;
                case MGesture.ActionType.自动隐藏任务栏:
                    AutoHideTaskBar();
                    break;
                //case ActionType.打开光驱:
                //    Win32API.CDDoorControl(Program.mainForm.CdVolume, true);
                //    break;
                //case ActionType.关闭光驱:
                //    Win32API.CDDoorControl(Program.mainForm.CdVolume, false);
                //    break;
                case MGesture.ActionType.左键按下:
                    Win32API.mouse_event((uint)MouseEventFlags.LeftDown, 0, 0, 0, 0);
                    break;
                case MGesture.ActionType.左键抬起:
                    Win32API.mouse_event((uint)MouseEventFlags.LeftUp, 0, 0, 0, 0);
                    break;
                case MGesture.ActionType.右键按下:
                    Win32API.mouse_event((uint)MouseEventFlags.RightDown, 0, 0, 0, 0);
                    break;
                case MGesture.ActionType.右键抬起:
                    Win32API.mouse_event((uint)MouseEventFlags.RightUp, 0, 0, 0, 0);
                    break;
                case MGesture.ActionType.鼠标速度切换:
                    if (Program.mainForm.MouseSpeed)
                    {
                        Win32API.SetMouseSpeed((uint)Program.mainForm.MouseSpeed1);
                        Program.mainForm.MouseSpeed = false;
                    }
                    else
                    {
                        Win32API.SetMouseSpeed((uint)Program.mainForm.MouseSpeed2);
                        Program.mainForm.MouseSpeed = true;
                    }
                    break;
            }
        }

        ///// <summary>
        ///// 显示统计信息
        ///// </summary>
        //private void ShowStat()
        //{
        //    Program.mainForm.ShowStat();
        //}

        ///// <summary>
        ///// 在IE的地址栏中加入前缀cf: 以使用chrome frame浏览
        ///// Test Only under IE8
        ///// </summary>
        //private void IECF()
        //{
        //    IntPtr hwnd = Win32API.GetForegroundWindow();
        //    if (hwnd != IntPtr.Zero)
        //    {
        //        Win32API.EnumChildWindows(hwnd, new Win32API.CallBack(EnumIEChild), 0);
        //    }
        //}

        ///// <summary>
        ///// 用于查找IE中的地址栏输出窗口
        ///// </summary>
        ///// <param name="hwnd"></param>
        ///// <param name="lp"></param>
        ///// <returns></returns>
        //private bool EnumIEChild(IntPtr hwnd, int lp)
        //{
        //    if (hwnd != IntPtr.Zero)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        Win32API.GetClassName(hwnd, sb, sb.Capacity);
        //        if (sb.ToString() == "Edit")
        //        {
        //            //const int WM_GETTEXT = 0x000D;
        //            Win32API.SendMessage(hwnd, 0x000D, sb.Capacity, sb);
        //            if (sb.ToString().StartsWith("http"))
        //            {
        //                Win32API.SendMessage(hwnd, 0x0102, 'c', 0);
        //                Win32API.SendMessage(hwnd, 0x0102, 'f', 0);
        //                Win32API.SendMessage(hwnd, 0x0102, ':', 0);

        //                Win32API.SetForegroundWindow(hwnd);
        //                KeyControl.KeyEvent(KeyEventType.KeyPress, Keys.Enter);
        //            }
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        private void MaxOrMin(bool isMax)
        {
            //IntPtr hwnd = Program.mainForm.hWndMouseIn;
            //IntPtr parent = Win32API.GetParent(hwnd);
            //while (parent != IntPtr.Zero)
            //{
            //    hwnd = parent;
            //    parent = Win32API.GetParent(hwnd);
            //}
            //if (hwnd != IntPtr.Zero)
            //{
                StringBuilder text = new StringBuilder();
            //    Win32API.GetWindowText(hwnd, text, 20);
            //    if (text.ToString() == "Program Manager")
            //    {
            ////检测是否在桌面上动作，是的话使用当前窗口

            IntPtr hwnd = Win32API.GetForegroundWindow();
            if (hwnd != IntPtr.Zero)
            {
                Win32API.GetWindowText(hwnd, text, 20);
                if (text.ToString() == "Program Manager")
                {
                    //当前是窗口是桌面，不做动作
                    return;
                }

                if (isMax)
                {
                    //最大化，如果是最小化就先恢复原大小
                    if (Win32API.IsIconic(hwnd))
                        Win32API.ShowWindowAsync(hwnd, Win32API.WindowState.SW_SHOWNORMAL);
                    else if ((Win32API.GetWindowLong(hwnd, -16) & 0x00010000) == 0x00010000)
                        //能够最大化才最大化
                        Win32API.ShowWindowAsync(hwnd, Win32API.WindowState.SW_SHOWMAXIMIZED);
                }
                else
                {
                    //最小化，如果是最大化就先恢复原大小
                    if (Win32API.IsZoomed(hwnd))
                        Win32API.ShowWindowAsync(hwnd, Win32API.WindowState.SW_SHOWNORMAL);
                    else if ((Win32API.GetWindowLong(hwnd, -16) & 0x00020000) == 0x00020000)
                        //能够小化才最小化
                        Win32API.ShowWindowAsync(hwnd, Win32API.WindowState.SW_SHOWMINIMIZED);
                }
            }
        }

        private void AutoHideTaskBar()
        {
            Win32API.APPBARDATA msgData = new Win32API.APPBARDATA();
            msgData.cbSize = (UInt32)System.Runtime.InteropServices.Marshal.SizeOf(msgData);

            // get taskbar state GetState = 0x00000004   SetState = 0x0000000a	
            UInt32 retVal = Win32API.SHAppBarMessage(0x00000004, ref msgData);
            //AutoHide		 = 0x00000001,
			//AlwaysOnTop = 0x00000002
            msgData = new Win32API.APPBARDATA();
            msgData.cbSize = (UInt32)System.Runtime.InteropServices.Marshal.SizeOf(msgData);
            if ((retVal & 0x00000001) != 0)
            {
                //show
                msgData.lParam = 0x00000002;
            }
            else
            {
                //auto hide
                msgData.lParam = 0x00000001 | 0x00000002;
            }
            // set taskbar state
            Win32API.SHAppBarMessage(0x0000000a, ref msgData);
        }
    }
}