using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace MGesture
{
    public delegate void VoidDel();

    public partial class MainForm : Form
    {
        //全局鼠标键盘钩子
        private MyHook.MyHooks hook;
        //手势识别
        private GestureReco gestureReco;
        private KeyReco keyReco;

        internal GestureReco GesReco { get { return gestureReco; } }
        internal KeyReco KeyReco { get { return keyReco; } }

        private FrmGestures frmG;
        private Graphics g;
        private FrmSetting frmSetting;
        private Point tempPoint = new Point();

        //private bool willOver = false;

        //窗口位置，默认右下角
        //private string direction = "→↓";

        private string fileName = null;

        private string configFileName = "MGesture.ini";

        public string ConfigFileName
        {
            get
            {
                if (configFileName == "MGesture.ini")
                    return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + configFileName;
                else return configFileName;
            }
            set { configFileName = value; }
        }

        public string FileName
        {
            get
            {
                if (fileName == null)
                    return Path.GetDirectoryName(Application.ExecutablePath) + "\\gestures.xml";
                else return fileName; ;
            }
            set { fileName = value; }
        }

        //性能计数器
        //private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        //private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        //private PerformanceCounter netCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec");
        //private PerformanceCounter netRecvCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec");
        //private PerformanceCounter diskCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
        //private PerformanceCounter diskWCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

        //设置窗口大小动作使用的宽度和高度值
        private int idealWindowSizeCx = 800;

        public int IdealWindowSizeCx
        {
            get { return idealWindowSizeCx; }
            set { idealWindowSizeCx = value; }
        }
        private int idealWindowSizeCy = 600;

        public int IdealWindowSizeCy
        {
            get { return idealWindowSizeCy; }
            set { idealWindowSizeCy = value; }
        }

        private int mouseSpeed1 = 10;
        private int mouseSpeed2 = 10;
        public int MouseSpeed1
        {
            get { return mouseSpeed1; }
            set { mouseSpeed1 = value; }
        }
        public int MouseSpeed2
        {
            get { return mouseSpeed2; }
            set { mouseSpeed2 = value; }
        }
        private bool mouseSpeed = false;
        public bool MouseSpeed
        {
            get { return mouseSpeed; }
            set { mouseSpeed = value; }
        }

        //private char cdVolume = 'c';
        ///// <summary>
        ///// 被控制的光驱的盘符
        ///// </summary>
        //public char CdVolume
        //{
        //    get { return cdVolume; }
        //    set { cdVolume = value; }
        //}
        //internal bool ShowExeTip = false;
        //private string netIf = "";

        //public string NetIf
        //{
        //    get { return netIf; }
        //    set 
        //    {
        //        netIf = value;
        //        netCounter.InstanceName = netIf;
        //        netRecvCounter.InstanceName = netIf;
        //    }
        //}

        //激活快捷键
        private Keys hotKey = Keys.F9;
        /// <summary>
        /// 激活快捷键
        /// </summary>
        public Keys HotKey
        {
            set { hotKey = value; }
            get { return hotKey; }
        }

        ///// <summary>
        ///// 最后一次鼠标按下时鼠标所在的窗口(并非一定是父窗口)
        ///// </summary>
        //public IntPtr hWndMouseIn
        //{
        //    get { return hook.HWndMouseIn; }
        //}

        //单个按键已被处理
        //private bool isHandled = false;
        //指示是否正由程序在模拟按键，防止被钩子截断
        private bool isSimulatedKey = false;
        //private bool isSimulatedMouse = false;

        /// <summary>
        /// 标记上一个响应了动作的按键
        /// </summary>
        private List<Keys> lastKeys = new List<Keys>();

        public MainForm()
        {
            InitializeComponent();
            //WindowState = FormWindowState.Minimized;

            //Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width - 10, Screen.PrimaryScreen.WorkingArea.Height - Height - 10);
            //((Control)this).MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void MainForm_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    if (e.Delta > 0)
        //    {
        //        //增加透明度
        //        Opacity += 0.05;
        //    }
        //    else if (e.Delta < 0)
        //    {
        //        if (Opacity > 0.1)
        //        {
        //            Opacity -= 0.05;
        //        }
        //    }
        //}

        private void tuichuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gestureReco.SaveGestures(fileName);
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            gestureReco = new GestureReco();
            keyReco = new KeyReco();
            hook = new MyHook.MyHooks();
            hook.OnMouseActivity += new MyHook.MyMouseEventHandler(hook_OnMouseActivity);
            hook.OnKeyUp += new MyHook.MyKeyEventHandler(hook_OnKeyUp);
            hook.OnKeyDown += new MyHook.MyKeyEventHandler(hook_OnKeyDown);

            //设置窗口具有穿透效果，不会捕捉到鼠标焦点
            //const int GWL_EXSTYLE = -20;
            //const int WS_EX_TRANSPARENT = 0x20;
            //const int WS_EX_LAYERED = 0x80000;
            //const int WS_EX_NOACTIVATE = 0x8000000;
            Win32API.SetWindowLong(Handle, -20, Win32API.GetWindowLong(Handle, -20) | 0x20 | 0x80000);

            if (!File.Exists(FileName))
            {
                gestureReco.ResetGestures();
                keyReco.ResetGestures();
                SaveGestures(FileName);
            }
            else
            {
                if (!gestureReco.LoadGestures(FileName) || !keyReco.LoadGestures(FileName))
                {
                    gestureReco.ResetGestures();
                    keyReco.ResetGestures();
                    //SaveGestures(FileName);
                    MessageBox.Show(this, "读取手势文件 gestures.xml 失败，将使用默认动作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //install hook
            toolStripMenuItem1_Click(null, null);
            //keyMenuItem_Click(null, null);

            if (File.Exists(ConfigFileName))
            {
                try
                {
                    string[] lines = File.ReadAllLines(ConfigFileName);
                    gestureReco.IsDrawLine = bool.Parse(lines[0]);
                    gestureReco.Pen = new Pen(Color.FromArgb(Int32.Parse(lines[1])), float.Parse(lines[2]));
                    gestureReco.LineLength = Int32.Parse(lines[3]);

                    keyReco.ModiferKey = (Keys)Enum.Parse(typeof(Keys), lines[4]);
                    idealWindowSizeCx = Int32.Parse(lines[5]);
                    idealWindowSizeCy = Int32.Parse(lines[6]);

                    //cdVolume = Char.Parse(lines[7]);

                    //try
                    //{
                    //    NetIf = lines[8];
                    //}
                    //catch (Exception) { }

                    hotKey = (Keys)Enum.Parse(typeof(Keys), lines[8]);
                    WinHotKey.SetHotKey(false, false, false, false, hotKey, this.Handle, 111);
                    toolStripMenuItem1.ShortcutKeyDisplayString = hotKey.ToString();

                    IsFullScreenStopReco = bool.Parse(lines[9]);
                    mouseSpeed1 = int.Parse(lines[10].Split('|')[0]);
                    mouseSpeed2 = int.Parse(lines[10].Split('|')[1]);
                    //ShowExeTip = bool.Parse(lines[11]);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "读取设置文件失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //Visible = false;
            //SW_HIDE   0
            if (gestureReco.IsDrawLine)
                WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;

            Win32API.ShowWindow(Program.mainForm.Handle, 0);
        }

        /// <summary>
        /// 添加一个例外
        /// </summary>
        /// <param name="program"></param>
        public void AddMouseException(string program)
        {
            hook.AddMouseException(program);
        }

        public void AddKeyException(string program)
        {
            hook.AddKeyException(program);
        }

        /// <summary>
        /// 全屏是否停止监测
        /// </summary>
        public bool IsFullScreenStopReco
        {
            get { return hook.IsFullScreenStopReco; }
            set { hook.IsFullScreenStopReco = value; }
        }

        /// <summary>
        /// 例外
        /// </summary>
        public HashSet<string> MouseExceptions { get { return hook.MouseExceptions; } }
        public HashSet<string> KeyExceptions { get { return hook.KeyExceptions; } }

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="program"></param>
        /// <param name="key"></param>
        /// <param name="handled"></param>
        void hook_OnKeyDown(string program, Keys key, ref bool handled)
        {
            if (!isSimulatedKey)
            {
                if (keyReco.IsRecording && key != keyReco.ModiferKey)
                {
                    if (!keyReco.PushKeyGesture(key))
                    {
                        //长度累积过长
                    }
                    handled = true;
                }
                else if (!KeyReco.IsRecording && key == keyReco.ModiferKey)
                {
                    //执行键按下，准备开始记录
                    handled = true;
                }
                else if (KeyReco.IsRecording && key == keyReco.ModiferKey)
                {
                    //执行键再次按下，准备停止记录
                    if (keyReco.Gesture.Length == 0)
                    {
                        //无按键序列时当作按执行键
                        handled = false;
                    }
                    else
                        handled = true;
                }
                else if (!keyReco.IsRecording && key != keyReco.ModiferKey)
                {
                    string keyString = "#";
                    bool isModifier = true;
                    bool isWinCombine = false;
                    //平时的按键，非 Alt Ctrl Shift Win，检查是否需要附加 Alt Ctrl Shift Win
                    if (key != Keys.LControlKey && key != Keys.LShiftKey && key != Keys.LMenu && key != Keys.RControlKey
                        && key != Keys.RShiftKey && key != Keys.RMenu && key != Keys.LWin && key != Keys.RWin)
                    {
                        isModifier = false;

                        if (Win32API.IsAltDown())
                            keyString += "Alt+";
                        if (Win32API.IsCtrlDown())
                            keyString += "Ctrl+";
                        if (Win32API.IsShiftDown())
                            keyString += "Shift+";
                        if (Win32API.IsWinDown())
                        {
                            keyString += "Win+";
                            isWinCombine = true;
                        }
                    }
                    keyString += Enum.GetName(typeof(System.Windows.Forms.Keys), key);

                    keyReco.ReInitWithoutRecording(program);
                    keyReco.PushSingleGesture(keyString);
                    //开始模拟按键，防止程序模拟的按键被截取
                    isSimulatedKey = true;

                    if (keyReco.ExecuteGesture(isModifier))
                    {
                        if (!isModifier && key != Keys.LButton && !isWinCombine)
                        {
                            handled = true;
                            lastKeys.Add(key);
                        }
                    }

                    //结束模拟按键
                    isSimulatedKey = false;
                }
            }
        }

        /// <summary>
        /// 捕获到键盘抬起事件
        /// </summary>
        /// <param name="program"></param>
        /// <param name="key"></param>
        void hook_OnKeyUp(string program, Keys key, ref bool handled)
        {
            if (!isSimulatedKey)
            {
                if (keyReco.IsRecording && key != keyReco.ModiferKey)
                {
                    handled = true;
                }
                else if (keyReco.IsRecording && key == keyReco.ModiferKey)
                {
                    //执行键抬起
                    //执行相应动作
                    isSimulatedKey = true;
                    keyReco.ExecuteGesture(false);
                    //{
                    //    MessageBox.Show(program);
                    //}
                    isSimulatedKey = false;

                    //无按键序列当作按下执行键
                    if (keyReco.Gesture.Length == 0)
                        handled = false;
                    else handled = true;
                }
                else if (!KeyReco.IsRecording && key == keyReco.ModiferKey)
                {
                    //执行抬起 开始记录
                    keyReco.ReInit(program);
                    handled = true;
                }
                else if (!keyReco.IsRecording && key != keyReco.ModiferKey)
                {
                    //如果是响应了动作的键就忽略其up事件
                    if (lastKeys.Contains(key))
                    {
                        handled = true;
                        lastKeys.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// 捕获到鼠标事件，进行处理
        /// </summary>
        /// <param name="sender">窗口的 exe 文件完整路径</param>
        /// <param name="e"></param>
        void hook_OnMouseActivity(string program, MouseButtons button, int clicks, int x, int y)
        {
            //if (!isSimulatedMouse)
            //{
            if (gestureReco.IsRecording && button == MouseButtons.None)
            {
                gestureReco.AddPoint(x, y);
            }
            else if (button == MouseButtons.Right && clicks == 1)
            {
                //开始记录轨迹
                tempPoint.X = x; tempPoint.Y = y;
                gestureReco.ReInit(tempPoint, program);

                if (gestureReco.IsDrawLine)
                {
                    //Program.mainForm.Visible = true;
                    //Program.mainForm.TopMost = true;
                    //SW_SHOWNA   8
                    Win32API.ShowWindow(Handle, 8);
                }
            }
            else if (gestureReco.IsRecording && button == MouseButtons.Right && clicks == -1)
            {
                //右键抬起
                //执行相应动作

                if (gestureReco.IsDrawLine)
                {
                    DrawClear();
                    //SW_HIDE   0
                    Win32API.ShowWindow(Program.mainForm.Handle, 0);
                }

                //开始模拟按键，防止程序模拟的按键被截取
                isSimulatedKey = true;
                //isSimulatedMouse = true;

                //execute the gesture
                if (gestureReco.ExecuteGesture())
                {
                    //if (ShowExeTip)
                    //    notifyIcon1.ShowBalloonTip(3500, "手势: " + gestureReco.Gesture + " 执行成功", "程序: " + gestureReco.ExeProgram, ToolTipIcon.Info);
                }
                //else if (ShowExeTip && gestureReco.Gesture.Length > 0)
                //{
                //    notifyIcon1.ShowBalloonTip(3500, "手势: " + gestureReco.Gesture + " 执行失败", "程序: " + gestureReco.ExeProgram, ToolTipIcon.Warning);
                //}

                //isSimulatedMouse = false;
                //结束模拟按键
                isSimulatedKey = false;

                if (gestureReco.Gesture.Length == 0)
                {
                    //无动作帮点鼠标右键. 使用独立线程，否则右键菜单出现有卡顿
                    System.Threading.ThreadPool.QueueUserWorkItem(rightClick);
                }
            }

            if (button == System.Windows.Forms.MouseButtons.Middle)
            {
                if (keyReco.IsRecording && clicks == -1)
                {
                    isSimulatedKey = true;
                    //中键抬起
                    //执行相应动作
                    keyReco.ExecuteGesture(false);
                    //{
                    //    MessageBox.Show(program);
                    //}
                    isSimulatedKey = false;
                }
                else if (!KeyReco.IsRecording && clicks == 1)
                {
                    //中键按下 开始记录
                    keyReco.ReInit(program);
                }
            }
            //}
        }

        private void rightClick(object o)
        {
            hook.IsStopMouseHook = true;
            MouseControl.MouseEvent(MouseEventFlags.RightClick, 0, 0);
            System.Threading.Thread.Sleep(10);
            hook.IsStopMouseHook = false;
        }

        /// <summary>
        /// 保存手势
        /// </summary>
        /// <param name="xmlFileName">XML 文件</param>
        public bool SaveGestures(string xmlFileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("MyGesture"); //root

                ///////////////////////Mouse Exceptions//////////////////////////
                XmlElement exception = xmlDoc.CreateElement("MouseExceptions");
                foreach (string fileName in MouseExceptions)
                {
                    XmlElement fileN = xmlDoc.CreateElement("FileName");
                    fileN.InnerText = fileName;
                    exception.AppendChild(fileN);
                }
                root.AppendChild(exception);
                /////////////////////////////////////////////////////////////

                ///////////////////////Key Exceptions//////////////////////////
                exception = xmlDoc.CreateElement("KeyExceptions");
                foreach (string fileName in KeyExceptions)
                {
                    XmlElement fileN = xmlDoc.CreateElement("FileName");
                    fileN.InnerText = fileName;
                    exception.AppendChild(fileN);
                }
                root.AppendChild(exception);
                /////////////////////////////////////////////////////////////

                ///////////////////////Mouse Program//////////////////////////////
                foreach (KeyValuePair<string, ProgramGesture> pair in gestureReco.Gestures)
                {
                    XmlElement program = xmlDoc.CreateElement("Program");
                    program.SetAttribute("FileName", pair.Key); //FileName
                    foreach (KeyValuePair<string, MyAction> gesture in pair.Value.Gestures)
                    {
                        XmlElement ges = xmlDoc.CreateElement("Gesture");
                        ges.SetAttribute("Name", gesture.Key);
                        ActionType type = gesture.Value.ActionType;
                        ges.SetAttribute("Action", Enum.GetName(typeof(ActionType), type));
                        ges.SetAttribute("Type", "M");
                        if (type == ActionType.KeyPress)
                        {
                            XmlElement key = xmlDoc.CreateElement("Key");
                            KeyAction ka = (KeyAction)(gesture.Value);
                            key.InnerText = Enum.GetName(typeof(Keys), ka.Key);
                            if (ka.Shift) key.SetAttribute("Shift", "true"); else key.SetAttribute("Shift", "false");
                            if (ka.Alt) key.SetAttribute("Alt", "true"); else key.SetAttribute("Alt", "false");
                            if (ka.Ctrl) key.SetAttribute("Ctrl", "true"); else key.SetAttribute("Ctrl", "false");
                            if (ka.WinKey) key.SetAttribute("WinKey", "true"); else key.SetAttribute("WinKey", "false");

                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.StartProgram)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            StartProAction ka = (StartProAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.MK)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            MKAction ka = (MKAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            key.SetAttribute("Times", ka.Times.ToString());
                            key.SetAttribute("Speed", ka.Speed.ToString());
                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.CS)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            CSAction ka = (CSAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            key.SetAttribute("Args", ka.Args);
                            key.SetAttribute("Dlls", ka.Dlls);
                            ges.AppendChild(key);
                        }
                        program.AppendChild(ges);
                    }
                    root.AppendChild(program);
                }
                /////////////////////////////////////////////////////////////

                ///////////////////////Key Program//////////////////////////////
                foreach (KeyValuePair<string, ProgramGesture> pair in keyReco.Gestures)
                {
                    XmlElement program = xmlDoc.CreateElement("Program");
                    program.SetAttribute("FileName", pair.Key); //FileName
                    foreach (KeyValuePair<string, MyAction> gesture in pair.Value.Gestures)
                    {
                        XmlElement ges = xmlDoc.CreateElement("Gesture");
                        ges.SetAttribute("Name", gesture.Key);
                        ActionType type = gesture.Value.ActionType;
                        ges.SetAttribute("Action", Enum.GetName(typeof(ActionType), type));
                        ges.SetAttribute("Type", "K");
                        if (type == ActionType.KeyPress)
                        {
                            XmlElement key = xmlDoc.CreateElement("Key");
                            KeyAction ka = (KeyAction)(gesture.Value);
                            key.InnerText = Enum.GetName(typeof(Keys), ka.Key);
                            if (ka.Shift) key.SetAttribute("Shift", "true"); else key.SetAttribute("Shift", "false");
                            if (ka.Alt) key.SetAttribute("Alt", "true"); else key.SetAttribute("Alt", "false");
                            if (ka.Ctrl) key.SetAttribute("Ctrl", "true"); else key.SetAttribute("Ctrl", "false");
                            if (ka.WinKey) key.SetAttribute("WinKey", "true"); else key.SetAttribute("WinKey", "false");

                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.StartProgram)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            StartProAction ka = (StartProAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.MK)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            MKAction ka = (MKAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            key.SetAttribute("Times", ka.Times.ToString());
                            key.SetAttribute("Speed", ka.Speed.ToString());
                            ges.AppendChild(key);
                        }
                        else if (type == ActionType.CS)
                        {
                            XmlElement key = xmlDoc.CreateElement("FileName");
                            CSAction ka = (CSAction)(gesture.Value);
                            key.InnerText = ka.FileName;
                            key.SetAttribute("Args", ka.Args);
                            key.SetAttribute("Dlls", ka.Dlls);
                            ges.AppendChild(key);
                        }
                        program.AppendChild(ges);
                    }
                    root.AppendChild(program);
                }
                ////////////////////////////////////////////////////////////////////////////////
                xmlDoc.AppendChild(root);
                xmlDoc.Save(xmlFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem1.Checked)
            {
                var err = hook.InstallMouseHook();
                if (err != 0)
                    MessageBox.Show(this, "鼠标激活失败: " + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                err = hook.InstallKeyHook();
                if (err != 0)
                    MessageBox.Show(this, "键盘激活失败: " + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                notifyIcon1.Icon = MGesture.Properties.Resources._35;
            }
            else
            {
                hook.UnInstallMouseHook();
                hook.UninstallKeyHook();

                notifyIcon1.Icon = MGesture.Properties.Resources._352;
            }
        }

        private void 设置OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Visible = true;
            //this.WindowState = FormWindowState.Normal;
            if (frmSetting == null || frmSetting.IsDisposed)
            {
                frmSetting = new FrmSetting(gestureReco, keyReco);
            }

            frmSetting.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (frmG == null || frmG.IsDisposed)
                frmG = new FrmGestures();
            frmG.Show();
        }

        public void SetGesture(string gesture)
        {
            Invoke(new VoidDel(delegate ()
            {
                if (frmG != null && !frmG.IsDisposed)
                    frmG.SetGesture(gesture);
            }));
        }

        /// <summary>
        /// 键盘动作输入框是否是焦点
        /// </summary>
        public bool IsKeyInputFocused
        {
            get
            {
                if (frmG != null && !frmG.IsDisposed)
                    return frmG.IsKeyInputFocused;
                else return false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (frmG == null || frmG.IsDisposed)
                frmG = new FrmGestures();
            frmG.Show();
        }

        //private void keyMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (keyMenuItem.Checked)
        //    {
        //        if (!hook.InstallKeyHook())
        //            MessageBox.Show(this, "钩子安装失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    else hook.UninstallKeyHook();
        //}

        ///// <summary>
        ///// 定时更新统计信息
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    RefreshAll();
        //}

        ///// <summary>
        ///// 显示统计信息
        ///// </summary>
        //public void ShowStat()
        //{
        //    if (Visible)
        //    {
        //        Visible = false;
        //        //WindowState = FormWindowState.Minimized;
        //        timer1.Stop();
        //    }
        //    else
        //    {
        //        RefreshAll();
        //        timer1.Start();
        //        Win32API.ShowWindow(Handle, 4);
        //        Visible = true;
        //        //WindowState = FormWindowState.Normal;
        //    }
        //}

        ///// <summary>
        ///// 更新统计信息
        ///// </summary>
        //private void RefreshAll()
        //{
        //    float cpuLoad = cpuCounter.NextValue();
        //    float ramLoad = ramCounter.NextValue();

        //    float diskRead = diskCounter.NextValue();
        //    //diskCounter.CounterName = "Disk Write Bytes/sec";
        //    float diskWrite = diskWCounter.NextValue();

        //    if (netCounter.InstanceName.Length > 0)
        //    {
        //        try
        //        {
        //            float netSent = netCounter.NextValue();
        //            //netCounter.CounterName = "Bytes Received/sec";
        //            float netRecv = netRecvCounter.NextValue();
        //            lblNetDown.Text = String.Format("下载: {0:F1} KB/S", netRecv / 1024.0f);
        //            lblNetUp.Text = String.Format("上传: {0:F1} KB/S", netSent / 1024.0f);
        //        }
        //        catch { }
        //    }

        //    lblCpu.Text = String.Format("CPU: {0:F2}%", cpuLoad);
        //    lblMem.Text = String.Format("内存: {0:F0} MB", ramLoad);
        //    lblDiskRead.Text = String.Format("磁盘读: {0:F1} MB/S", diskRead / 1048576);
        //    lblDiskWrite.Text = String.Format("磁盘写: {0:F1} MB/S", diskWrite / 1048576);
        //}

        /// <summary>
        /// 用方向键控制窗口的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MainForm_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int x = Location.X, y = Location.Y;
        //    //↓←→↑
        //    if (e.KeyData == Keys.Left)
        //    {
        //        if (direction.Contains("→"))
        //        {
        //            direction = direction.Replace('→', '←');
        //            x = 10;
        //        }
        //    }
        //    else if (e.KeyData == Keys.Right)
        //    {
        //        if (direction.Contains("←"))
        //        {
        //            direction = direction.Replace('←', '→');
        //            x = Screen.PrimaryScreen.Bounds.Width - Width - 10;
        //        }
        //    }
        //    else if (e.KeyData == Keys.Up)
        //    {
        //        if (direction.Contains("↓"))
        //        {
        //            direction = direction.Replace('↓', '↑');
        //            y = 10;
        //        }
        //    }
        //    else if (e.KeyData == Keys.Down)
        //    {
        //        if (direction.Contains("↑"))
        //        {
        //            direction = direction.Replace('↑', '↓');
        //            y = Screen.PrimaryScreen.WorkingArea.Height - Height - 10;
        //        }
        //    }

        //    if (Location.X != x || Location.Y != y)
        //        Location = new Point(x, y);
        //}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.UnInstallMouseHook();
            WinHotKey.RemoveHotKey(this.Handle, 111);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog(this);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    int id = Int32.Parse(m.WParam.ToString());
                    if (id == 111)
                    {
                        toolStripMenuItem1.Checked = !toolStripMenuItem1.Checked;
                        toolStripMenuItem1_Click(null, null);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public void ExecuteGesture(MyAction action)
        {
            //(new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ExecuteAction))).Start(action);
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
            {
                try
                {
                    action.execute();
                }
                catch (Exception e)
                {
                    Invoke(new VoidDel(delegate ()
                    {
                        string msg = e.Message;
                        if (e is CSharpInterpreter.ExecuteException && e.InnerException != null)
                            msg = e.InnerException.Message;
                        notifyIcon1.ShowBalloonTip(10000, "动作执行失败", msg, ToolTipIcon.Warning);
                    }));
                }
            }), action);
        }

        //private void ExecuteAction(object action)
        //{
        //    try
        //    {
        //        ((MyAction)action).execute();
        //    }
        //    catch (Exception e)
        //    {
        //        Invoke(new VoidDel(delegate()
        //        {
        //            string msg = e.Message;
        //            if (e is CSharpInterpreter.ExecuteException && e.InnerException != null)
        //                msg = e.InnerException.Message;
        //            notifyIcon1.ShowBalloonTip(10000, "动作执行失败", msg, ToolTipIcon.Warning);
        //        }));
        //    }
        //}

        public static string GetClipText()
        {
            string text = "";
            System.Threading.Thread staThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    try
                    {
                        text = Clipboard.GetText();
                    }
                    catch { }
                }));
            staThread.SetApartmentState(System.Threading.ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return text;
        }

        /// <summary>
        /// 画轨迹
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x">绝对坐标x</param>
        /// <param name="y">绝对坐标y</param>
        /// <param name="xx"></param>
        /// <param name="yy"></param>
        public void DrawLine(Pen pen, int x, int y, int xx, int yy)
        {
            if (g == null)
                g = CreateGraphics();

            tempPoint.X = x; tempPoint.Y = y;
            Point p1 = PointToClient(tempPoint);
            tempPoint.X = xx; tempPoint.Y = yy;
            Point p2 = PointToClient(tempPoint);
            g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        public void DrawClear()
        {
            if (g != null)
                g.Clear(TransparencyKey);
        }
    }
}