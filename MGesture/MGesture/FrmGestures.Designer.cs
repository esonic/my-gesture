namespace MGesture
{
    partial class FrmGestures
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components=new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGestures));
            comboBox1=new System.Windows.Forms.ComboBox();
            button1=new System.Windows.Forms.Button();
            toolTip1=new System.Windows.Forms.ToolTip(components);
            shancToolStripMenuItem=new System.Windows.Forms.Button();
            button2=new System.Windows.Forms.Button();
            button3=new System.Windows.Forms.Button();
            listView1=new System.Windows.Forms.ListView();
            colType=new System.Windows.Forms.ColumnHeader();
            colWait=new System.Windows.Forms.ColumnHeader();
            contextMenuStrip2=new System.Windows.Forms.ContextMenuStrip(components);
            AddtoolStripMenuItem=new System.Windows.Forms.ToolStripMenuItem();
            deleteToolStripMenuItem=new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3=new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem2=new System.Windows.Forms.ToolStripMenuItem();
            label1=new System.Windows.Forms.Label();
            tabControl1=new System.Windows.Forms.TabControl();
            tabPage1=new System.Windows.Forms.TabPage();
            tabControl2=new System.Windows.Forms.TabControl();
            mouseActionPage=new System.Windows.Forms.TabPage();
            keyActionPage=new System.Windows.Forms.TabPage();
            listView2=new System.Windows.Forms.ListView();
            columnHeader1=new System.Windows.Forms.ColumnHeader();
            columnHeader2=new System.Windows.Forms.ColumnHeader();
            tabPage2=new System.Windows.Forms.TabPage();
            tabControl3=new System.Windows.Forms.TabControl();
            mouseExceptionPage=new System.Windows.Forms.TabPage();
            listBox1=new System.Windows.Forms.ListBox();
            contextMenuStrip1=new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem4=new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem5=new System.Windows.Forms.ToolStripMenuItem();
            keyExceptionPage=new System.Windows.Forms.TabPage();
            listBox2=new System.Windows.Forms.ListBox();
            label2=new System.Windows.Forms.Label();
            openFileDialog1=new System.Windows.Forms.OpenFileDialog();
            saveFileDialog1=new System.Windows.Forms.SaveFileDialog();
            contextMenuStrip2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl2.SuspendLayout();
            mouseActionPage.SuspendLayout();
            keyActionPage.SuspendLayout();
            tabPage2.SuspendLayout();
            tabControl3.SuspendLayout();
            mouseExceptionPage.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            keyExceptionPage.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            comboBox1.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled=true;
            comboBox1.Location=new System.Drawing.Point(72, 13);
            comboBox1.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            comboBox1.Name="comboBox1";
            comboBox1.Size=new System.Drawing.Size(491, 33);
            comboBox1.TabIndex=0;
            comboBox1.SelectedIndexChanged+=comboBox1_SelectedIndexChanged;
            comboBox1.MouseEnter+=comboBox1_MouseEnter;
            // 
            // button1
            // 
            button1.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Right;
            button1.Location=new System.Drawing.Point(575, 10);
            button1.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            button1.Name="button1";
            button1.Size=new System.Drawing.Size(100, 48);
            button1.TabIndex=1;
            button1.Text="添加(&A)";
            toolTip1.SetToolTip(button1, "为一个新的程序设置手势");
            button1.UseVisualStyleBackColor=true;
            button1.Click+=button1_Click;
            // 
            // shancToolStripMenuItem
            // 
            shancToolStripMenuItem.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Right;
            shancToolStripMenuItem.Location=new System.Drawing.Point(685, 10);
            shancToolStripMenuItem.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            shancToolStripMenuItem.Name="shancToolStripMenuItem";
            shancToolStripMenuItem.Size=new System.Drawing.Size(100, 48);
            shancToolStripMenuItem.TabIndex=16;
            shancToolStripMenuItem.Text="删除(&D)";
            toolTip1.SetToolTip(shancToolStripMenuItem, "删除选中的程序及相应的手势");
            shancToolStripMenuItem.UseVisualStyleBackColor=true;
            shancToolStripMenuItem.Click+=shancToolStripMenuItem_Click;
            // 
            // button2
            // 
            button2.Anchor=System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right;
            button2.Location=new System.Drawing.Point(525, 633);
            button2.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            button2.Name="button2";
            button2.Size=new System.Drawing.Size(125, 44);
            button2.TabIndex=17;
            button2.Text="保存(&S)";
            toolTip1.SetToolTip(button2, "保存手势设置");
            button2.UseVisualStyleBackColor=true;
            button2.Click+=toolStripMenuItem1_Click;
            // 
            // button3
            // 
            button3.Anchor=System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right;
            button3.Location=new System.Drawing.Point(660, 633);
            button3.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            button3.Name="button3";
            button3.Size=new System.Drawing.Size(125, 44);
            button3.TabIndex=18;
            button3.Text="重置(&R)";
            toolTip1.SetToolTip(button3, "重置手势设置");
            button3.UseVisualStyleBackColor=true;
            button3.Click+=重置RToolStripMenuItem_Click;
            // 
            // listView1
            // 
            listView1.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colType, colWait });
            listView1.ContextMenuStrip=contextMenuStrip2;
            listView1.FullRowSelect=true;
            listView1.GridLines=true;
            listView1.HeaderStyle=System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listView1.Location=new System.Drawing.Point(5, 6);
            listView1.Margin=new System.Windows.Forms.Padding(5, 8, 5, 8);
            listView1.Name="listView1";
            listView1.ShowItemToolTips=true;
            listView1.Size=new System.Drawing.Size(780, 452);
            listView1.TabIndex=13;
            listView1.UseCompatibleStateImageBehavior=false;
            listView1.View=System.Windows.Forms.View.Details;
            // 
            // colType
            // 
            colType.Text="鼠标手势";
            colType.Width=187;
            // 
            // colWait
            // 
            colWait.Text="动作";
            colWait.Width=198;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.ImageScalingSize=new System.Drawing.Size(24, 24);
            contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { AddtoolStripMenuItem, deleteToolStripMenuItem, toolStripMenuItem3, toolStripMenuItem2 });
            contextMenuStrip2.Name="contextMenuStrip2";
            contextMenuStrip2.Size=new System.Drawing.Size(146, 106);
            contextMenuStrip2.Opening+=contextMenuStrip2_Opening;
            // 
            // AddtoolStripMenuItem
            // 
            AddtoolStripMenuItem.Name="AddtoolStripMenuItem";
            AddtoolStripMenuItem.Size=new System.Drawing.Size(145, 32);
            AddtoolStripMenuItem.Text="添加(&P)";
            AddtoolStripMenuItem.ToolTipText="添加至选中项前";
            AddtoolStripMenuItem.Click+=AddtoolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name="deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size=new System.Drawing.Size(145, 32);
            deleteToolStripMenuItem.Text="删除(&D)";
            deleteToolStripMenuItem.Click+=deleteToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name="toolStripMenuItem3";
            toolStripMenuItem3.Size=new System.Drawing.Size(142, 6);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name="toolStripMenuItem2";
            toolStripMenuItem2.Size=new System.Drawing.Size(145, 32);
            toolStripMenuItem2.Text="全选(&A)";
            toolStripMenuItem2.Click+=toolStripMenuItem2_Click;
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Location=new System.Drawing.Point(13, 21);
            label1.Margin=new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name="label1";
            label1.Size=new System.Drawing.Size(50, 25);
            label1.TabIndex=15;
            label1.Text="程序";
            // 
            // tabControl1
            // 
            tabControl1.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location=new System.Drawing.Point(0, 0);
            tabControl1.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabControl1.Name="tabControl1";
            tabControl1.SelectedIndex=0;
            tabControl1.Size=new System.Drawing.Size(812, 621);
            tabControl1.TabIndex=16;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tabControl2);
            tabPage1.Controls.Add(shancToolStripMenuItem);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(button1);
            tabPage1.Location=new System.Drawing.Point(4, 34);
            tabPage1.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabPage1.Name="tabPage1";
            tabPage1.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabPage1.Size=new System.Drawing.Size(804, 583);
            tabPage1.TabIndex=0;
            tabPage1.Text="手势动作";
            tabPage1.UseVisualStyleBackColor=true;
            // 
            // tabControl2
            // 
            tabControl2.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            tabControl2.Controls.Add(mouseActionPage);
            tabControl2.Controls.Add(keyActionPage);
            tabControl2.Location=new System.Drawing.Point(0, 67);
            tabControl2.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabControl2.Name="tabControl2";
            tabControl2.SelectedIndex=0;
            tabControl2.Size=new System.Drawing.Size(798, 504);
            tabControl2.TabIndex=17;
            // 
            // mouseActionPage
            // 
            mouseActionPage.Controls.Add(listView1);
            mouseActionPage.Location=new System.Drawing.Point(4, 34);
            mouseActionPage.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            mouseActionPage.Name="mouseActionPage";
            mouseActionPage.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            mouseActionPage.Size=new System.Drawing.Size(790, 466);
            mouseActionPage.TabIndex=0;
            mouseActionPage.Text="鼠标动作";
            mouseActionPage.UseVisualStyleBackColor=true;
            // 
            // keyActionPage
            // 
            keyActionPage.Controls.Add(listView2);
            keyActionPage.Location=new System.Drawing.Point(4, 34);
            keyActionPage.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            keyActionPage.Name="keyActionPage";
            keyActionPage.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            keyActionPage.Size=new System.Drawing.Size(790, 466);
            keyActionPage.TabIndex=1;
            keyActionPage.Text="键盘动作";
            keyActionPage.UseVisualStyleBackColor=true;
            // 
            // listView2
            // 
            listView2.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2 });
            listView2.ContextMenuStrip=contextMenuStrip2;
            listView2.FullRowSelect=true;
            listView2.GridLines=true;
            listView2.HeaderStyle=System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listView2.Location=new System.Drawing.Point(5, 6);
            listView2.Margin=new System.Windows.Forms.Padding(5, 8, 5, 8);
            listView2.Name="listView2";
            listView2.ShowItemToolTips=true;
            listView2.Size=new System.Drawing.Size(780, 452);
            listView2.TabIndex=14;
            listView2.UseCompatibleStateImageBehavior=false;
            listView2.View=System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text="按键序列";
            columnHeader1.Width=187;
            // 
            // columnHeader2
            // 
            columnHeader2.Text="动作";
            columnHeader2.Width=198;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tabControl3);
            tabPage2.Controls.Add(label2);
            tabPage2.Location=new System.Drawing.Point(4, 34);
            tabPage2.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabPage2.Name="tabPage2";
            tabPage2.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabPage2.Size=new System.Drawing.Size(804, 583);
            tabPage2.TabIndex=1;
            tabPage2.Text="例外";
            tabPage2.UseVisualStyleBackColor=true;
            // 
            // tabControl3
            // 
            tabControl3.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            tabControl3.Controls.Add(mouseExceptionPage);
            tabControl3.Controls.Add(keyExceptionPage);
            tabControl3.Location=new System.Drawing.Point(5, 56);
            tabControl3.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            tabControl3.Name="tabControl3";
            tabControl3.SelectedIndex=0;
            tabControl3.Size=new System.Drawing.Size(794, 521);
            tabControl3.TabIndex=2;
            // 
            // mouseExceptionPage
            // 
            mouseExceptionPage.Controls.Add(listBox1);
            mouseExceptionPage.Location=new System.Drawing.Point(4, 34);
            mouseExceptionPage.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            mouseExceptionPage.Name="mouseExceptionPage";
            mouseExceptionPage.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            mouseExceptionPage.Size=new System.Drawing.Size(786, 483);
            mouseExceptionPage.TabIndex=0;
            mouseExceptionPage.Text="鼠标例外";
            mouseExceptionPage.UseVisualStyleBackColor=true;
            // 
            // listBox1
            // 
            listBox1.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            listBox1.ContextMenuStrip=contextMenuStrip1;
            listBox1.FormattingEnabled=true;
            listBox1.ItemHeight=25;
            listBox1.Location=new System.Drawing.Point(5, 6);
            listBox1.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            listBox1.Name="listBox1";
            listBox1.SelectionMode=System.Windows.Forms.SelectionMode.MultiExtended;
            listBox1.Size=new System.Drawing.Size(776, 454);
            listBox1.TabIndex=0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize=new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem4, toolStripMenuItem5 });
            contextMenuStrip1.Name="contextMenuStrip2";
            contextMenuStrip1.Size=new System.Drawing.Size(146, 68);
            contextMenuStrip1.Opening+=contextMenuStrip1_Opening;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name="toolStripMenuItem4";
            toolStripMenuItem4.Size=new System.Drawing.Size(145, 32);
            toolStripMenuItem4.Text="添加(&P)";
            toolStripMenuItem4.ToolTipText="添加至选中项前";
            toolStripMenuItem4.Click+=toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name="toolStripMenuItem5";
            toolStripMenuItem5.Size=new System.Drawing.Size(145, 32);
            toolStripMenuItem5.Text="删除(&D)";
            toolStripMenuItem5.Click+=toolStripMenuItem5_Click;
            // 
            // keyExceptionPage
            // 
            keyExceptionPage.Controls.Add(listBox2);
            keyExceptionPage.Location=new System.Drawing.Point(4, 34);
            keyExceptionPage.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            keyExceptionPage.Name="keyExceptionPage";
            keyExceptionPage.Padding=new System.Windows.Forms.Padding(5, 6, 5, 6);
            keyExceptionPage.Size=new System.Drawing.Size(786, 483);
            keyExceptionPage.TabIndex=1;
            keyExceptionPage.Text="键盘例外";
            keyExceptionPage.UseVisualStyleBackColor=true;
            // 
            // listBox2
            // 
            listBox2.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            listBox2.ContextMenuStrip=contextMenuStrip1;
            listBox2.FormattingEnabled=true;
            listBox2.ItemHeight=25;
            listBox2.Location=new System.Drawing.Point(5, 6);
            listBox2.Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            listBox2.Name="listBox2";
            listBox2.SelectionMode=System.Windows.Forms.SelectionMode.MultiExtended;
            listBox2.Size=new System.Drawing.Size(776, 454);
            listBox2.TabIndex=1;
            // 
            // label2
            // 
            label2.AutoSize=true;
            label2.Location=new System.Drawing.Point(13, 17);
            label2.Margin=new System.Windows.Forms.Padding(5, 0, 5, 0);
            label2.Name="label2";
            label2.Size=new System.Drawing.Size(444, 25);
            label2.TabIndex=1;
            label2.Text="如果希望程序不受“通用设置”的影响可以添加到这里";
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt="exe";
            openFileDialog1.Filter="可执行文件|*.exe";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt="xml";
            saveFileDialog1.FileName="gestures.xml";
            saveFileDialog1.Filter="XML文件|*.xml";
            // 
            // FrmGestures
            // 
            AutoScaleDimensions=new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            ClientSize=new System.Drawing.Size(805, 692);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(tabControl1);
            Icon=(System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin=new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name="FrmGestures";
            StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
            Text="手势管理";
            Load+=FrmGestures_Load;
            contextMenuStrip2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabControl2.ResumeLayout(false);
            mouseActionPage.ResumeLayout(false);
            keyActionPage.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabControl3.ResumeLayout(false);
            mouseExceptionPage.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            keyExceptionPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colWait;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem AddtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button shancToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage mouseActionPage;
        private System.Windows.Forms.TabPage keyActionPage;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage mouseExceptionPage;
        private System.Windows.Forms.TabPage keyExceptionPage;
        internal System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}