namespace MKRecorder
{
    partial class FrmReplayOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReplayOption));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.udTimes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.udTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(110, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(191, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Replay Times";
            // 
            // udTimes
            // 
            this.udTimes.Location = new System.Drawing.Point(132, 28);
            this.udTimes.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTimes.Name = "udTimes";
            this.udTimes.Size = new System.Drawing.Size(87, 20);
            this.udTimes.TabIndex = 3;
            this.udTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Replay Speed";
            // 
            // udSpeed
            // 
            this.udSpeed.DecimalPlaces = 1;
            this.udSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udSpeed.Location = new System.Drawing.Point(132, 63);
            this.udSpeed.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.udSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(87, 20);
            this.udSpeed.TabIndex = 5;
            this.udSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(58, 99);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Replay Mouse Move";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmReplayOption
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(278, 170);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.udSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.udTimes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmReplayOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MKRecorder - Replay Option";
            ((System.ComponentModel.ISupportInitialize)(this.udTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udTimes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udSpeed;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}