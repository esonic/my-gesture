using System;
using System.Collections.Generic;
using System.Text;

namespace MKRecorder
{
    //键鼠记录器
    public class MKRecorder
    {
        private bool isReplaying = false;

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsReplaying
        {
            get
            {
                return isReplaying;
            }
        }

        private int nowIndex;
        private DateTime startTime;

        private int replayed = 0;
        private int replayTime = 0;
        private float replaySpeed = 1.0f;

        public event EventHandler Stoped;

        private List<IMKAction> actions = new List<IMKAction>();

        /// <summary>
        /// 键鼠动作
        /// </summary>
        public List<IMKAction> Actions
        {
            get { return actions; }
        }

        /// <summary>
        /// 添加记录的动作
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(IMKAction action)
        {
            if (actions.Count > 0)
            {
                if (actions[actions.Count - 1].Time < action.Time)
                {
                    actions.Add(action);
                }
            }
            else actions.Add(action);
        }

        /// <summary>
        /// 清空记录的动作
        /// </summary>
        public void Clear()
        {
            actions.Clear();
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="times">播放次数</param>
        public void StartReplay(int times, float speed)
        {
            isReplaying = true;
            nowIndex = 0;
            replayed = 0;
            replayTime = times;
            replaySpeed = speed;
            startTime = DateTime.Now;

            (new System.Threading.Thread(new System.Threading.ThreadStart(Timer_Elapsed))).Start();
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopReplay()
        {
            if (isReplaying)
            {
                isReplaying = false;

                if (Stoped != null)
                    Stoped(this, null);
            }
        }

        private void Timer_Elapsed()
        {
            while (isReplaying)
            {
                //鼠标位置设置
                if (nowIndex < actions.Count)
                {
                    IMKAction mp = actions[nowIndex];

                    if ((DateTime.Now - startTime).TotalMilliseconds * replaySpeed >= mp.Time)
                    {
                        mp.Execute();
                        nowIndex++;
                    }
                }
                else
                {
                    replayed++;
                    if (replayed < replayTime)
                    {
                        nowIndex = 0;
                        startTime = DateTime.Now;
                    }
                    else StopReplay();
                }
                System.Threading.Thread.Sleep(2);
            }
        }

        /// <summary>
        /// 读取记录文件
        /// </summary>
        /// <param name="file"></param>
        public void LoadRecords(string file)
        {
            //try
            //{
            string[] lines = System.IO.File.ReadAllLines(file);

            Clear();
            foreach (string line in lines)
            {
                AddAction(BuilderAction(line));
            }
            //}
            //catch (Exception ex)
            //{
            //MessageBox.Show(this, "Failed to load records file\r\n" + ex.Message, "MKRecorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
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
    }
}
