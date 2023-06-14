namespace MGesture
{
    partial class AboutBox1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
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
            tableLayoutPanel=new System.Windows.Forms.TableLayoutPanel();
            labelProductName=new System.Windows.Forms.Label();
            labelVersion=new System.Windows.Forms.Label();
            labelCopyright=new System.Windows.Forms.Label();
            textBoxDescription=new System.Windows.Forms.TextBox();
            okButton=new System.Windows.Forms.Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount=1;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 3);
            tableLayoutPanel.Controls.Add(okButton, 1, 4);
            tableLayoutPanel.Dock=System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location=new System.Drawing.Point(10, 10);
            tableLayoutPanel.Margin=new System.Windows.Forms.Padding(4, 4, 4, 4);
            tableLayoutPanel.Name="tableLayoutPanel";
            tableLayoutPanel.RowCount=5;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.47291F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.47291F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.34884F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.44186F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.35296F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel.Size=new System.Drawing.Size(344, 180);
            tableLayoutPanel.TabIndex=0;
            // 
            // labelProductName
            // 
            labelProductName.Dock=System.Windows.Forms.DockStyle.Fill;
            labelProductName.Location=new System.Drawing.Point(7, 0);
            labelProductName.Margin=new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelProductName.MaximumSize=new System.Drawing.Size(0, 20);
            labelProductName.Name="labelProductName";
            labelProductName.Size=new System.Drawing.Size(333, 19);
            labelProductName.TabIndex=19;
            labelProductName.Text="产品名称";
            labelProductName.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock=System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location=new System.Drawing.Point(7, 19);
            labelVersion.Margin=new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelVersion.MaximumSize=new System.Drawing.Size(0, 20);
            labelVersion.Name="labelVersion";
            labelVersion.Size=new System.Drawing.Size(333, 19);
            labelVersion.TabIndex=0;
            labelVersion.Text="版本";
            labelVersion.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock=System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location=new System.Drawing.Point(7, 38);
            labelCopyright.Margin=new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelCopyright.MaximumSize=new System.Drawing.Size(0, 20);
            labelCopyright.Name="labelCopyright";
            labelCopyright.Size=new System.Drawing.Size(333, 20);
            labelCopyright.TabIndex=21;
            labelCopyright.Text="版权";
            labelCopyright.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock=System.Windows.Forms.DockStyle.Fill;
            textBoxDescription.Location=new System.Drawing.Point(7, 78);
            textBoxDescription.Margin=new System.Windows.Forms.Padding(7, 4, 4, 4);
            textBoxDescription.Multiline=true;
            textBoxDescription.Name="textBoxDescription";
            textBoxDescription.ReadOnly=true;
            textBoxDescription.ScrollBars=System.Windows.Forms.ScrollBars.Both;
            textBoxDescription.Size=new System.Drawing.Size(333, 69);
            textBoxDescription.TabIndex=23;
            textBoxDescription.TabStop=false;
            textBoxDescription.Text="说明";
            // 
            // okButton
            // 
            okButton.Anchor=System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right;
            okButton.DialogResult=System.Windows.Forms.DialogResult.Cancel;
            okButton.Location=new System.Drawing.Point(252, 155);
            okButton.Margin=new System.Windows.Forms.Padding(4, 4, 4, 4);
            okButton.Name="okButton";
            okButton.Size=new System.Drawing.Size(88, 21);
            okButton.TabIndex=24;
            okButton.Text="确定(&O)";
            // 
            // AboutBox1
            // 
            AcceptButton=okButton;
            AutoScaleDimensions=new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            ClientSize=new System.Drawing.Size(364, 200);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin=new System.Windows.Forms.Padding(4, 4, 4, 4);
            MaximizeBox=false;
            MinimizeBox=false;
            Name="AboutBox1";
            Padding=new System.Windows.Forms.Padding(10, 10, 10, 10);
            ShowIcon=false;
            ShowInTaskbar=false;
            StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
            Text="AboutBox";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button okButton;
    }
}
