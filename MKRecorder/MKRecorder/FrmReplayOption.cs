using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MKRecorder
{
    public partial class FrmReplayOption : Form
    {
        public FrmReplayOption()
        {
            InitializeComponent();
        }

        public void SetParam(int times, float speed, bool replayMove)
        {
            udTimes.Value = times;
            udSpeed.Value = (decimal)speed;
            checkBox1.Checked = replayMove;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.mainFrm.replaySpeed = (float)udSpeed.Value;
            Program.mainFrm.replayTimes = (int)udTimes.Value;
            Program.mainFrm.replayMove = checkBox1.Checked;

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
