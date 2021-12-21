using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MGesture
{
    /// <summary>
    /// 执行键鼠脚本
    /// </summary>
    class MKAction : MyAction
    {
        /// <summary>
        /// 执行次数
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 执行速度
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        public MKAction(string fileName, int times, float speed)
            : base(ActionType.MK)
        {
            FileName = fileName;
            Times = times;
            Speed = speed;
        }

        public override void execute()
        {
            MKRecorder.MKRecorder rec = new MKRecorder.MKRecorder();
            rec.LoadRecords(FileName);
            rec.StartReplay(Times, Speed);
        }

        public override string ToString()
        {
            return "脚本 " + FileName + " 次数 " + Times + " 速率 " + Speed.ToString("0.0");
        }
    }
}
