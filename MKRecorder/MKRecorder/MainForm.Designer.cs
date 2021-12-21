namespace MKRecorder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startRecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.chkMove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "mkr";
            this.openFileDialog1.Filter = "MKRecorder files|*.mkr";
            this.openFileDialog1.Title = "Select a mkr file";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MKRecorder";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startRecToolStripMenuItem,
            this.stopRecToolStripMenuItem,
            this.chkMove,
            this.toolStripSeparator3,
            this.startReplayToolStripMenuItem,
            this.stopReplayToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 264);
            // 
            // startRecToolStripMenuItem
            // 
            this.startRecToolStripMenuItem.Name = "startRecToolStripMenuItem";
            this.startRecToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.startRecToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startRecToolStripMenuItem.Text = "Start &Record";
            this.startRecToolStripMenuItem.Click += new System.EventHandler(this.startRecToolStripMenuItem_Click);
            // 
            // stopRecToolStripMenuItem
            // 
            this.stopRecToolStripMenuItem.Enabled = false;
            this.stopRecToolStripMenuItem.Name = "stopRecToolStripMenuItem";
            this.stopRecToolStripMenuItem.ShortcutKeyDisplayString = "F6";
            this.stopRecToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopRecToolStripMenuItem.Text = "Stop R&ecord";
            this.stopRecToolStripMenuItem.Click += new System.EventHandler(this.stopRecToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // startReplayToolStripMenuItem
            // 
            this.startReplayToolStripMenuItem.Name = "startReplayToolStripMenuItem";
            this.startReplayToolStripMenuItem.ShortcutKeyDisplayString = "F7";
            this.startReplayToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startReplayToolStripMenuItem.Text = "Start Re&play";
            this.startReplayToolStripMenuItem.Click += new System.EventHandler(this.startReplayToolStripMenuItem_Click);
            // 
            // stopReplayToolStripMenuItem
            // 
            this.stopReplayToolStripMenuItem.Enabled = false;
            this.stopReplayToolStripMenuItem.Name = "stopReplayToolStripMenuItem";
            this.stopReplayToolStripMenuItem.ShortcutKeyDisplayString = "F8";
            this.stopReplayToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopReplayToolStripMenuItem.Text = "Stop Repla&y";
            this.stopReplayToolStripMenuItem.Click += new System.EventHandler(this.stopReplayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Replay Op&tion";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.openToolStripMenuItem.Text = "&Open record...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveToolStripMenuItem.Text = "&Save record...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "mkr";
            this.saveFileDialog1.Filter = "MKRecorder files|*.mkr";
            this.saveFileDialog1.Title = "Select a file";
            // 
            // chkMove
            // 
            this.chkMove.Checked = true;
            this.chkMove.CheckOnClick = true;
            this.chkMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMove.Name = "chkMove";
            this.chkMove.Size = new System.Drawing.Size(157, 22);
            this.chkMove.Text = "Record &Move";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 58);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "MKRecorder";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startRecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRecToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem startReplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopReplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem chkMove;
    }
}

