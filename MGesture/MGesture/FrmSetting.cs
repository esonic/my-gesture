using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace MGesture
{
    public partial class FrmSetting : Form
    {
        private GestureReco gestureReco;
        private KeyReco keyReco;
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

        public FrmSetting(GestureReco gesReco, KeyReco keyReco)
        {
            InitializeComponent();
            this.gestureReco = gesReco;
            this.keyReco = keyReco;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = gestureReco.IsDrawLine;
            lblColor.BackColor = gestureReco.Pen.Color;
            numericUpDown1.Value = (decimal)gestureReco.Pen.Width;
            numericUpDown2.Value = gestureReco.LineLength;
            //textBox1.Text = Program.mainForm.CdVolume.ToString();
            chkFullCheck.Checked = Program.mainForm.IsFullScreenStopReco;

            numericUpDown3.Maximum = Screen.PrimaryScreen.Bounds.Width;
            numericUpDown4.Maximum = Screen.PrimaryScreen.Bounds.Height;
            numericUpDown3.Value = Program.mainForm.IdealWindowSizeCx;
            numericUpDown4.Value = Program.mainForm.IdealWindowSizeCy;

            //PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            //string[] instancenames = category.GetInstanceNames();
            //if (instancenames.Length > 0)
            //{
            //    comboBox1.Items.AddRange(instancenames);
            //    if (comboBox1.Items.Contains(Program.mainForm.NetIf))
            //    {
            //        comboBox1.SelectedItem = Program.mainForm.NetIf;
            //    }
            //}

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                string name = Enum.GetName(typeof(Keys), key);
                if (!cbKey.Items.Contains(name))
                    cbKey.Items.Add(name);
            }

            cbKey.SelectedItem = Enum.GetName(typeof(Keys), keyReco.ModiferKey);

            if (isAutoStart)
                AutoMenu.Checked = true;
            else AutoMenu.Checked = false;
        }

        /// <summary>
        /// 根据设置调整是否自动启动
        /// </summary>
        private void AutoStart()
        {
            try
            {
                string strName = Application.ExecutablePath;
                string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);
                if (AutoMenu.Checked && !isAutoStart)
                {
                    if (!File.Exists(strName))
                        return;
                    Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (Rkey == null)
                        Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    Rkey.SetValue(strnewName, strName);
                }
                else if (!AutoMenu.Checked && isAutoStart)
                {
                    Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    Rkey.DeleteValue(strnewName, false);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "设置自动启动失败，请确保使用管理员权限运行程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool isAutoStart
        {
            get
            {
                bool isAuto = false;
                string path = Application.ExecutablePath;
                string name = path.Substring(path.LastIndexOf("\\") + 1);

                Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
                if (Rkey != null)
                {
                    if (Rkey.GetValue(name) != null && Rkey.GetValue(name).ToString().ToLower() == path.ToLower())
                        isAuto = true;
                }
                return isAuto;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text.Trim().Length != 1 || !Char.IsLetter(textBox1.Text[0]))
            //{
            //    MessageBox.Show(this, "光驱盘符格式错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    textBox1.Focus();
            //    return;
            //}

            AutoStart();
            gestureReco.Pen = new Pen(lblColor.BackColor, (float)numericUpDown1.Value);
            gestureReco.LineLength = (int)(numericUpDown2.Value);
            gestureReco.IsDrawLine = checkBox1.Checked;
            if (gestureReco.IsDrawLine)
            {
                Program.mainForm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                Program.mainForm.WindowState = FormWindowState.Normal;
                Program.mainForm.ShowInTaskbar = false;
            }

            if (cbKey.SelectedItem != null)
                keyReco.ModiferKey = (Keys)(Enum.Parse(typeof(Keys), cbKey.SelectedItem.ToString()));

            Program.mainForm.IdealWindowSizeCx = (int)numericUpDown3.Value;
            Program.mainForm.IdealWindowSizeCy = (int)numericUpDown4.Value;

            //Program.mainForm.CdVolume = textBox1.Text[0];
            Program.mainForm.IsFullScreenStopReco = chkFullCheck.Checked;
            //try
            //{
            //    if (comboBox1.SelectedItem.ToString() != null && comboBox1.SelectedItem.ToString().Trim().Length > 0)
            //        Program.mainForm.NetIf = comboBox1.SelectedItem.ToString();
            //}
            //catch (Exception) { MessageBox.Show(this, "设置网络接口失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            var color = lblColor.BackColor.A + "," + lblColor.BackColor.R + "," + lblColor.BackColor.G + "," + lblColor.BackColor.B;
            string text = checkBox1.Checked.ToString() + "\r\n" + color + "\r\n" + numericUpDown1.Value.ToString()
                + "\r\n" + gestureReco.LineLength.ToString() + "\r\n" + Enum.GetName(typeof(Keys), keyReco.ModiferKey) + "\r\n" +
                Program.mainForm.IdealWindowSizeCx.ToString() + "\r\n" + Program.mainForm.IdealWindowSizeCy.ToString() + "\r\n" +
                /*Program.mainForm.CdVolume*/"F" + "\r\n" + Program.mainForm.HotKey + "\r\n" + Program.mainForm.IsFullScreenStopReco + "\r\n" + Program.mainForm.MouseSpeed1 + "|" + Program.mainForm.MouseSpeed2 +
                "\r\n是否显示轨迹、轨迹颜色、轨迹宽度、最短可识别长度、执行键、理想窗口大小宽度、理想窗口大小高度、被控制光驱盘符、激活快捷键、全屏停止识别、鼠标速度切换(1-20)";

            File.WriteAllText(ConfigFileName, text);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //AutoMenu.Checked = isAutoStart;
            //checkBox1.Checked = gestureReco.IsDrawLine;
            //lblColor.BackColor = gestureReco.Pen.Color;
            //numericUpDown1.Value = (decimal)gestureReco.Pen.Width;

            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = lblColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
                lblColor.BackColor = colorDialog1.Color;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                lblColor.Enabled = true;
                lblColor.BackColor = gestureReco.Pen.Color;
                numericUpDown1.Enabled = true;
            }
            else
            {
                lblColor.Enabled = false;
                lblColor.BackColor = Color.Gray;
                numericUpDown1.Enabled = false;
            }
        }
    }
}