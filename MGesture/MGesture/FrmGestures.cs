using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MGesture
{
    public partial class FrmGestures : Form
    {
        private FrmAddGesture frmAdd;

        public FrmGestures()
        {
            InitializeComponent();
        }

        private void FrmGestures_Load(object sender, EventArgs e)
        {
            RefreshAllData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProgramData();

            if (comboBox1.SelectedItem.ToString() == "通用设置")
                shancToolStripMenuItem.Enabled = false;
            else shancToolStripMenuItem.Enabled = true;
        }

        public void RefreshProgramData()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            string text = comboBox1.SelectedItem.ToString();
            if (comboBox1.SelectedItem.ToString() == "通用设置")
                text = "";
            //mouse
            foreach (KeyValuePair<string, ProgramGesture> pair in Program.mainForm.GesReco.Gestures)
            {
                if (pair.Key != text)
                    continue;
                else
                {
                    foreach (KeyValuePair<string, MyAction> gesture in pair.Value.Gestures)
                    {
                        ListViewItem it = null;
                        ActionType type = gesture.Value.ActionType;
                        if (type == ActionType.KeyPress)
                        {
                            KeyAction ka = (KeyAction)(gesture.Value);
                            string str = Enum.GetName(typeof(Keys), ka.Key);
                            if (ka.WinKey) str = "Win + " + str;
                            if (ka.Alt) str = "Alt + " + str;
                            if (ka.Ctrl) str = "Ctrl + " + str;
                            if (ka.Shift) str = "Shift + " + str;
                            str = "按键: " + str;
                            it = new ListViewItem(new string[] { gesture.Key, str });
                        }
                        else if (type == ActionType.StartProgram)
                        {
                            StartProAction ka = (StartProAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "运行: " + ka.FileName });
                        }
                        else if (type == ActionType.MK)
                        {
                            MKAction ka = (MKAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + ka });
                        }
                        else if (type == ActionType.CS)
                        {
                            CSAction ka = (CSAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + ka });
                        }
                        else
                        {
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + Enum.GetName(typeof(ActionType), type) });
                        }
                        listView1.Items.Add(it);
                    }
                }
            }

            //key
            foreach (KeyValuePair<string, ProgramGesture> pair in Program.mainForm.KeyReco.Gestures)
            {
                if (pair.Key != text)
                    continue;
                else
                {
                    foreach (KeyValuePair<string, MyAction> gesture in pair.Value.Gestures)
                    {
                        ListViewItem it = null;
                        ActionType type = gesture.Value.ActionType;
                        if (type == ActionType.KeyPress)
                        {
                            KeyAction ka = (KeyAction)(gesture.Value);
                            string str = Enum.GetName(typeof(Keys), ka.Key);
                            if (ka.WinKey) str = "Win + " + str;
                            if (ka.Alt) str = "Alt + " + str;
                            if (ka.Ctrl) str = "Ctrl + " + str;
                            if (ka.Shift) str = "Shift + " + str;
                            str = "按键: " + str;
                            it = new ListViewItem(new string[] { gesture.Key, str });
                        }
                        else if (type == ActionType.StartProgram)
                        {
                            StartProAction ka = (StartProAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "运行: " + ka.FileName });
                        }
                        else if (type == ActionType.MK)
                        {
                            MKAction ka = (MKAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + ka });
                        }
                        else if (type == ActionType.CS)
                        {
                            CSAction ka = (CSAction)(gesture.Value);
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + ka });
                        }
                        else
                        {
                            it = new ListViewItem(new string[] { gesture.Key, "执行: " + Enum.GetName(typeof(ActionType), type) });
                        }
                        listView2.Items.Add(it);
                    }
                }
            }
        }

        public void RefreshExceptionData()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (string fileName in Program.mainForm.MouseExceptions)
            {
                listBox1.Items.Add(fileName);
            }
            foreach (string fileName in Program.mainForm.KeyExceptions)
            {
                listBox2.Items.Add(fileName);
            }
        }

        public void RefreshAllData()
        {
            comboBox1.Items.Clear();
            //mouse
            foreach (KeyValuePair<string, ProgramGesture> pair in Program.mainForm.GesReco.Gestures)
            {
                if (!String.IsNullOrEmpty(pair.Key))
                    comboBox1.Items.Add(pair.Key);
                else comboBox1.Items.Add("通用设置");
            }
            //key
            foreach (KeyValuePair<string, ProgramGesture> pair in Program.mainForm.KeyReco.Gestures)
            {
                if (!String.IsNullOrEmpty(pair.Key) && !comboBox1.Items.Contains(pair.Key))
                    comboBox1.Items.Add(pair.Key);
                //else comboBox1.Items.Add("通用设置");
            }
            comboBox1.SelectedIndex = 0;

            RefreshExceptionData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "可执行文件|*.exe";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Add(openFileDialog1.FileName);
                comboBox1.SelectedItem = openFileDialog1.FileName;
            }
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(comboBox1.SelectedItem.ToString(), comboBox1, 3000);
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (tabControl2.SelectedTab == mouseActionPage)
            {
                if (listView1.SelectedItems.Count < 1)
                    deleteToolStripMenuItem.Enabled = false;
                else deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (listView2.SelectedItems.Count < 1)
                    deleteToolStripMenuItem.Enabled = false;
                else deleteToolStripMenuItem.Enabled = true;
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (tabControl3.SelectedTab == mouseExceptionPage)
            {
                foreach (int index in listBox1.SelectedIndices)
                {
                    Program.mainForm.MouseExceptions.Remove(listBox1.Items[index].ToString());
                    listBox1.Items.RemoveAt(index);
                }
            }
            else
            {
                foreach (int index in listBox2.SelectedIndices)
                {
                    Program.mainForm.KeyExceptions.Remove(listBox2.Items[index].ToString());
                    listBox2.Items.RemoveAt(index);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (tabControl3.SelectedTab == mouseExceptionPage)
            {
                if (listBox1.SelectedItems.Count < 1)
                    toolStripMenuItem5.Enabled = false;
                else toolStripMenuItem5.Enabled = true;
            }
            else
            {
                if (listBox2.SelectedItems.Count < 1)
                    toolStripMenuItem5.Enabled = false;
                else toolStripMenuItem5.Enabled = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == mouseActionPage)
            {
                foreach (int index in listView1.SelectedIndices)
                {
                    string program = "";
                    if (comboBox1.SelectedItem.ToString() != "通用设置")
                        program = comboBox1.SelectedItem.ToString();
                    Program.mainForm.GesReco.Gestures[program].Remove(listView1.Items[index].SubItems[0].Text);
                }
            }
            else
            {
                foreach (int index in listView2.SelectedIndices)
                {
                    string program = "";
                    if (comboBox1.SelectedItem.ToString() != "通用设置")
                        program = comboBox1.SelectedItem.ToString();
                    Program.mainForm.KeyReco.Gestures[program].Remove(listView2.Items[index].SubItems[0].Text);
                }
            }
            RefreshProgramData();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == mouseActionPage)
            {
                foreach (ListViewItem it in listView1.Items)
                    it.Selected = true;
            }
            else
            {
                foreach (ListViewItem it in listView2.Items)
                    it.Selected = true;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "可执行文件|*.exe";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (tabControl3.SelectedTab == mouseExceptionPage)
                {
                    listBox1.Items.Add(openFileDialog1.FileName);
                    Program.mainForm.AddMouseException(openFileDialog1.FileName);
                }
                else
                {
                    listBox2.Items.Add(openFileDialog1.FileName);
                    Program.mainForm.AddKeyException(openFileDialog1.FileName);
                }
            }
        }

        private void AddtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (frmAdd == null || frmAdd.IsDisposed)
                frmAdd = new FrmAddGesture(this);
            if (comboBox1.SelectedItem.ToString() == "通用设置")
                frmAdd.Program = "";
            else frmAdd.Program = comboBox1.SelectedItem.ToString();

            if (tabControl2.SelectedTab == mouseActionPage)
            {
                frmAdd.SetGestrueType(false);  
            }
            else
            {
                frmAdd.SetGestrueType(true);
            }
            frmAdd.ShowDialog(this);
        }

        public void SetGesture(string gesture)
        {
            if (frmAdd != null && !frmAdd.IsDisposed)
                frmAdd.SetGesture(gesture);
        }

        public bool IsKeyInputFocused
        {
            get
            {
                if (frmAdd != null && !frmAdd.IsDisposed)
                    return frmAdd.IsKeyInputFocused;
                else return false;
            }
        }

        private void 重置RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "将恢复默认手势设置，所有自定义手势将被删除\r\n确认恢复默认设置吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Program.mainForm.GesReco.ResetGestures();
                Program.mainForm.KeyReco.ResetGestures();
                RefreshAllData();
            }
        }

        //private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.Filter = "XML文件|*.xml";
        //    if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Program.mainForm.GesReco.LoadGestures(openFileDialog1.FileName);
        //        Program.mainForm.KeyReco.LoadGestures(openFileDialog1.FileName);
        //        Program.mainForm.FileName = openFileDialog1.FileName;
        //        RefreshAllData();
        //    }
        //}

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Program.mainForm.SaveGestures(Program.mainForm.FileName))
                MessageBox.Show(this, "保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (Program.mainForm.SaveGestures(saveFileDialog1.FileName))
        //        {
        //            MessageBox.Show(this, "保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            Program.mainForm.FileName = saveFileDialog1.FileName;
        //        }
        //    }
        //}

        private void shancToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "确定删除选中的程序及其手势吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Program.mainForm.GesReco.RemoveProgramGesture(comboBox1.SelectedItem.ToString());
                Program.mainForm.KeyReco.RemoveProgramGesture(comboBox1.SelectedItem.ToString());
                RefreshAllData();
            }
        }
    }
}