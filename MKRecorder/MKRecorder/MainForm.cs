using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MKRecorder
{
    public partial class MainForm : Form
    {
        private delegate void VoidDel();

        //程序路径
        internal string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        //快捷键
        private Keys startRecHotKey = Keys.F3;
        private Keys stopRecHotKey = Keys.F4;
        private Keys startRepHotKey = Keys.F5;
        private Keys stopRepHotKey = Keys.F6;
    
        //钩子
        private MyHook.MyHooks hook = new MyHook.MyHooks();

        //记录器
        private MKRecorder recorder = new MKRecorder();

        private FrmReplayOption opw;
        //记录起始时间
        private DateTime startTime;
        //是否记录中
        private bool isRecording = false;
        //重放次数
        internal int replayTimes = 1;
        internal float replaySpeed = 1.0f;
        internal bool replayMove = true;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按键抬起
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handled"></param>
        void hook_OnKeyUp(Keys key, ref bool handled)
        {
            if (isRecording && !IsHotKey(key))
            {
                recorder.AddAction(new KeyAction(key, true, (int)(DateTime.Now - startTime).TotalMilliseconds));
            }
        }

        /// <summary>
        /// 按键按下
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handled"></param>
        void hook_OnKeyDown(Keys key, ref bool handled)
        {
            if (isRecording && !IsHotKey(key))
            {
                recorder.AddAction(new KeyAction(key, false, (int)(DateTime.Now - startTime).TotalMilliseconds));
            }
        }

        private bool IsHotKey(Keys key)
        {
            if (key == startRecHotKey || key == startRepHotKey || key == stopRecHotKey || key == stopRepHotKey)
                return true;
            else return false;
        }

        /// <summary>
        /// 鼠标动作
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void hook_OnMouseActivity(MyHook.MouseEventFlags flag, int x, int y)
        {
            if (isRecording)
            {
                if (flag == MyHook.MouseEventFlags.MO && chkMove.Checked)
                    recorder.AddAction(new MouseAction(flag, x, y, (int)(DateTime.Now - startTime).TotalMilliseconds));
                else if (flag != MyHook.MouseEventFlags.MO)
                    recorder.AddAction(new MouseAction(flag, x, y, (int)(DateTime.Now - startTime).TotalMilliseconds));
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);

            LoadConfig();

            WinHotKey.SetHotKey(false, false, false, false, startRecHotKey, Handle, 99);
            WinHotKey.SetHotKey(false, false, false, false, stopRecHotKey, Handle, 100);
            WinHotKey.SetHotKey(false, false, false, false, startRepHotKey, Handle, 101);
            WinHotKey.SetHotKey(false, false, false, false, stopRepHotKey, Handle, 102);

            startRecToolStripMenuItem.ShortcutKeyDisplayString = startRecHotKey.ToString();
            stopRecToolStripMenuItem.ShortcutKeyDisplayString = stopRecHotKey.ToString();
            startReplayToolStripMenuItem.ShortcutKeyDisplayString = startRepHotKey.ToString();
            stopReplayToolStripMenuItem.ShortcutKeyDisplayString = stopRepHotKey.ToString();

            recorder.Stoped += new EventHandler(Replay_Stoped);

            hook.OnMouseActivity += new MyHook.MyMouseEventHandler(hook_OnMouseActivity);
            hook.OnKeyDown += new MyHook.MyKeyEventHandler(hook_OnKeyDown);
            hook.OnKeyUp += new MyHook.MyKeyEventHandler(hook_OnKeyUp);

            hook.InstallKeyHook();
            hook.InstallMouseHook();

            Visible = false;
        }

        void Replay_Stoped(object sender, EventArgs e)
        {
            Invoke(new VoidDel(Stopped));
        }

        /// <summary>
        /// 播放停止
        /// </summary>
        private void Stopped()
        {
            startRecToolStripMenuItem.Enabled = true;
            stopRecToolStripMenuItem.Enabled = false;
            startReplayToolStripMenuItem.Enabled = true;
            stopReplayToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;

            notifyIcon1.Icon = Properties.Resources.blue;
            notifyIcon1.ShowBalloonTip(2000, "Replay Stopped", "Stop replaying mouse & key actions", ToolTipIcon.Info);

            System.Media.SystemSounds.Exclamation.Play();
            System.GC.Collect();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.UninstallKeyHook();
            hook.UnInstallMouseHook();
        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    int id = Int32.Parse(m.WParam.ToString());
                    if (id == 99)
                    {
                        //Rec
                        startRecToolStripMenuItem_Click(null, null);
                    }
                    else if (id == 100)
                    {
                        //Stop Rec
                        stopRecToolStripMenuItem_Click(null, null);
                    }
                    else if (id == 101)
                    {
                        //Replay
                        startReplayToolStripMenuItem_Click(null, null);
                    }
                    else if (id == 102)
                    {
                        //Stop Replay
                        stopReplayToolStripMenuItem_Click(null, null);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void startRecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isRecording || recorder.IsReplaying) return;

            System.Media.SystemSounds.Asterisk.Play();

            startRecToolStripMenuItem.Enabled = false;
            stopRecToolStripMenuItem.Enabled = true;
            startReplayToolStripMenuItem.Enabled = false;
            stopReplayToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;

            notifyIcon1.Icon = Properties.Resources.red;
            notifyIcon1.ShowBalloonTip(2000, "Record Started", "Start recording mouse & key actions", ToolTipIcon.Info);

            recorder.Clear();
            startTime = DateTime.Now;
            isRecording = true;
        }

        private void stopRecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isRecording) return;

            System.Media.SystemSounds.Exclamation.Play();

            startRecToolStripMenuItem.Enabled = true;
            stopRecToolStripMenuItem.Enabled = false;
            startReplayToolStripMenuItem.Enabled = true;
            stopReplayToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;

            notifyIcon1.Icon = Properties.Resources.blue;
            notifyIcon1.ShowBalloonTip(2000, "Record Stopped", "Stop recording mouse & key actions", ToolTipIcon.Info);

            isRecording = false;
        }

        private void startReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isRecording || recorder.IsReplaying) return;

            System.Media.SystemSounds.Asterisk.Play();

            startRecToolStripMenuItem.Enabled = false;
            stopRecToolStripMenuItem.Enabled = false;
            startReplayToolStripMenuItem.Enabled = false;
            stopReplayToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;

            notifyIcon1.Icon = Properties.Resources.green;
            notifyIcon1.ShowBalloonTip(2000, "Replay Started", "Start replaying mouse & key actions", ToolTipIcon.Info);

            recorder.StartReplay(replayTimes, replaySpeed, replayMove);
        }

        private void stopReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recorder.StopReplay();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                SaveRecords(saveFileDialog1.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                LoadRecords(openFileDialog1.FileName);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "MKRecorder V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version +
            "\r\nA mouse & keyboard action record tool.\r\n\r\n©2010 esonic All rights reserved.\r\n\r\nEmail: esonice@gmail.com      ", "MKRecorder - About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 从字符串创建动作
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static IMKAction BuilderAction(string str)
        {
            IMKAction action = null;

            if (str.StartsWith("M"))
            {
                action = new MouseAction(str);
            }
            else if (str.StartsWith("K"))
            {
                action = new KeyAction(str);
            }

            return action;
        }

        public void LoadConfig()
        {
            try
            {
                string file = path + "\\config.ini";
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);

                    startRecHotKey = (Keys)Enum.Parse(typeof(Keys), lines[0]);
                    stopRecHotKey = (Keys)Enum.Parse(typeof(Keys), lines[1]);
                    startRepHotKey = (Keys)Enum.Parse(typeof(Keys), lines[2]);
                    stopRepHotKey = (Keys)Enum.Parse(typeof(Keys), lines[3]);

                    //replayTimes = int.Parse(lines[4]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load config file\r\n" + ex.Message, "MKRecorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 读取记录文件
        /// </summary>
        /// <param name="file"></param>
        public void LoadRecords(string file)
        {
            try
            {
                string[] lines = File.ReadAllLines(file);

                recorder.Clear();
                foreach (string line in lines)
                {
                    recorder.AddAction(BuilderAction(line));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load records file\r\n" + ex.Message, "MKRecorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存记录文件
        /// </summary>
        public void SaveRecords(string file)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (IMKAction action in recorder.Actions)
                {
                    sb.AppendLine(action.ToString());
                }

                File.WriteAllText(file, sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to save records file\r\n" + ex.Message, "MKRecorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (opw == null || opw.IsDisposed)
            {
                opw = new FrmReplayOption();
                opw.SetParam(replayTimes, replaySpeed, replayMove);
                opw.Show(this);
            }
        }
    }
}
