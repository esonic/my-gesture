using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MGesture
{
    public partial class FrmAddGesture : Form
    {
        private string program;
        private FrmGestures frm;
        private bool isKey;

        public string Program
        {
            get { return program; }
            set { program = value; }
        }

        public FrmAddGesture(FrmGestures frm)
        {
            this.frm = frm;
            InitializeComponent();
        }

        /// <summary>
        /// 设置动作类型
        /// </summary>
        /// <param name="isKey"></param>
        public void SetGestrueType(bool isKey)
        {
            this.isKey = isKey;
        }

        /// <summary>
        /// 设置动作，用来添加
        /// </summary>
        /// <param name="gesture"></param>
        public void SetGesture(string gesture)
        {
            if (isKey)
            {
               // if (lblGesture.Focused)
                //{
                //lblGesture.ForeColor = Color.Black;
                lblGesture.Text = gesture;

                //组合键时不能使用键盘动作
                if (gesture.Contains("+"))
                {
                    radioButton2.Enabled = false;
                    if (radioButton2.Checked)
                        radioButton1.Checked = true;
                }
                else radioButton2.Enabled = true;

                button1.Enabled = true;
                this.Refresh();
                //}
            }
            else
            {
                lblGesture.Text = gesture;
                button1.Enabled = true;
                this.Refresh();
            }
        }

        public bool IsKeyInputFocused
        {
            get
            {
                return lblGesture.Focused;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox1.Enabled = true;

                cbShift.Enabled = false;
                cbCtrl.Enabled = false;
                cbAlt.Enabled = false;
                cbWin.Enabled = false;
                cbKey.Enabled = false;
            }
            else if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;

                cbShift.Enabled = true;
                cbCtrl.Enabled = true;
                cbAlt.Enabled = true;
                cbWin.Enabled = true;
                cbKey.Enabled = true;
            }
        }

        private void FrmAddGesture_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            foreach (ActionType type in Enum.GetValues(typeof(ActionType)))
            {
                if (type != ActionType.KeyPress && type != ActionType.StartProgram && type != ActionType.MK && type != ActionType.CS)
                {
                    string name = Enum.GetName(typeof(ActionType), type);
                    if (!comboBox1.Items.Contains(name))
                        comboBox1.Items.Add(name);
                }
            }
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                string name = Enum.GetName(typeof(Keys), key);
                if (!cbKey.Items.Contains(name))
                    cbKey.Items.Add(name);
            }

            //lblGesture.ForeColor = Color.Gray;
            if (isKey)
            {
                label2.Text = "按键序列";
                lblGesture.Text = "请以执行键开始和结束输入按键序列或键入单一或组合按键(组合按键无法使用键盘动作)";
                lblGesture.SelectionStart = 0;
                lblGesture.ScrollToCaret();
            }
            else
            {
                label2.Text = "鼠标手势";
                lblGesture.Text = "请绘制一个鼠标手势";
            }

            comboBox1.SelectedIndex = 0;
            cbKey.SelectedIndex = 0;

            radioButton1_CheckedChanged(null, null);
            lblGesture.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (radioButton1.Checked)
                {
                    if (isKey)
                        MGesture.Program.mainForm.KeyReco.AddProgramGesture(program, lblGesture.Text, new MyAction((ActionType)Enum.Parse(typeof(ActionType), comboBox1.SelectedItem.ToString())));
                    else
                        MGesture.Program.mainForm.GesReco.AddProgramGesture(program, lblGesture.Text, new MyAction((ActionType)Enum.Parse(typeof(ActionType), comboBox1.SelectedItem.ToString())));
                }
                else if (radioButton2.Checked)
                {
                    if (isKey)
                        MGesture.Program.mainForm.KeyReco.AddProgramGesture(program, lblGesture.Text,
                            new KeyAction((Keys)(Enum.Parse(typeof(Keys), cbKey.SelectedItem.ToString())), cbShift.Checked, cbCtrl.Checked, cbAlt.Checked, cbWin.Checked));
                    else
                        MGesture.Program.mainForm.GesReco.AddProgramGesture(program, lblGesture.Text,
                            new KeyAction((Keys)(Enum.Parse(typeof(Keys), cbKey.SelectedItem.ToString())), cbShift.Checked, cbCtrl.Checked, cbAlt.Checked, cbWin.Checked));
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (isKey)
                    MGesture.Program.mainForm.KeyReco.AddProgramGesture(program, lblGesture.Text, new StartProAction(textBox1.Text));
                else
                    MGesture.Program.mainForm.GesReco.AddProgramGesture(program, lblGesture.Text, new StartProAction(textBox1.Text));
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (isKey)
                    MGesture.Program.mainForm.KeyReco.AddProgramGesture(program, lblGesture.Text, new MKAction(textBox2.Text, (int)numericUpDown1.Value, (float)numericUpDown2.Value));
                else
                    MGesture.Program.mainForm.GesReco.AddProgramGesture(program, lblGesture.Text, new MKAction(textBox2.Text, (int)numericUpDown1.Value, (float)numericUpDown2.Value));
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                if (isKey)
                    MGesture.Program.mainForm.KeyReco.AddProgramGesture(program, lblGesture.Text, new CSAction(txtCs.Text, txtArgs.Text, txtDll.Text));
                else
                    MGesture.Program.mainForm.GesReco.AddProgramGesture(program, lblGesture.Text, new CSAction(txtCs.Text, txtArgs.Text, txtDll.Text));
            }

            frm.RefreshProgramData();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "请选择要运行的程序或文件";
            openFileDialog1.Filter = "可执行文件|*.exe|所有文件|*.*";
            openFileDialog1.DefaultExt = "exe";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "请选择键鼠控制脚本";
            openFileDialog1.Filter = "脚本文件|*.mkr|所有文件|*.*";
            openFileDialog1.DefaultExt = "mkr";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "请选择C#脚本文件";
            openFileDialog1.Filter = "C#文件|*.cs|所有文件|*.*";
            openFileDialog1.DefaultExt = "cs";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCs.Text = openFileDialog1.FileName;
            }
        }
    }
}