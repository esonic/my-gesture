using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Text;

namespace MGesture
{
    /// <summary>
    /// 识别一段轨迹，原理：
    /// 不断对两两点进行分析，判断它们的方向，如果与前一次分析的方向相同或者不符合任何方向（上下左右）
    /// 则不做处理，如果与前一个方向不同，则保存该方向，最后对方向序列进行判断。
    /// </summary>
    public class GestureReco
    {
        //线条最短长度
        private int lineLength = 10;
        /// <summary>
        /// 线条最短可识别长度
        /// </summary>
        public int LineLength
        {
            get { return lineLength; }
            set { lineLength = value; }
        }

        //已注册的程序
        private Dictionary<string, ProgramGesture> gestures;

        internal Dictionary<string, ProgramGesture> Gestures
        {
            get { return gestures; }
        }

        private bool isDrawLine = true;

        public bool IsDrawLine
        {
            get 
            {
                return isDrawLine && System.Environment.OSVersion.Version.Major >= 6 && Win32API.DwmIsCompositionEnabled();
            }
            set { isDrawLine = value; }
        }

        /// <summary>
        /// 移动的基本方向
        /// </summary>
        private const char LEFT = '←';
        private const char RIGHT = '→';
        private const char UP = '↑';
        private const char DOWN = '↓';
        private const char NONE = 'N';

        /// <summary>
        /// 当次分析的动作
        /// </summary>
        private StringBuilder gesture = new StringBuilder();

        public string Gesture
        {
            get { return gesture.ToString(); }
        }

        /// <summary>
        /// 上一个点
        /// </summary>
        private Point lastPoint;

        /// <summary>
        /// 上一个方向
        /// </summary>
        private char lastDirection;

        /// <summary>
        /// 目标程序
        /// </summary>
        private string program;
        public string ExeProgram { get { return program; } }

        /// <summary>
        /// 是否在记录点
        /// </summary>
        private bool isRecording;

        public bool IsRecording
        {
            get { return isRecording; }
        }

        //屏幕的DC
        //private Graphics g;

        //轨迹的颜色和宽度
        private Pen pen;

        public Pen Pen
        {
            get { return pen; }
            set
            {
                pen = value;
                //pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GestureReco()
        {
            ReInit(new Point(0, 0), "");
            //g = Graphics.FromHdc(Win32API.CreateDC("DISPLAY", null, null, (IntPtr)null));
            //g = Program.mainForm.CreateGraphics();
            gestures = new Dictionary<string, ProgramGesture>();
            pen = new Pen(Color.GreenYellow, 3);
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            isRecording = false;
        }

        ~GestureReco()
        {
            //g.ReleaseHdc();
            //pen.Dispose();
        }

        /// <summary>
        /// 重初始化，使得对象可以重用
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="program">目标程序</param>
        public void ReInit(Point startPoint, string program)
        {
            lastPoint.X = startPoint.X;
            lastPoint.Y = startPoint.Y;
            this.program = program;
            lastDirection = NONE;
            gesture.Remove(0, gesture.Length);
            isRecording = true;

            //if (isDrawLine)
            //{
                //Program.mainForm.Visible = true;
                //Program.mainForm.TopMost = true;
                //SW_SHOWNA   8
                //Win32API.ShowWindow(Program.mainForm.Handle, 8);
            //}
        }

        /// <summary>
        /// 将一个方向保存下来
        /// </summary>
        /// <param name="direction"></param>
        private void PushMouseGesture(char direction)
        {
            //需要判断与上一方向的关系再做决定
            if (lastDirection == direction)
                return;
            else
            {
                gesture.Append(direction);
                lastDirection = direction;
            }
        }

        /// <summary>
        /// 与上一个点做比较，得出方向
        /// </summary>
        /// <param name="nowPoint">新的点</param>
        public void AddPoint(int xx, int yy)
        {
            int x = xx - lastPoint.X; //横向长度
            int y = yy - lastPoint.Y; //纵向长度

            if (x * x + y * y > LineLength) //至少线段长10
            {
                if (x > Math.Abs(y) && x > 0)
                    PushMouseGesture(RIGHT);
                else if (Math.Abs(x) > Math.Abs(y) && x < 0)
                    PushMouseGesture(LEFT);
                else if (y > Math.Abs(x) && y > 0)
                    PushMouseGesture(DOWN);
                else if (Math.Abs(y) > Math.Abs(x) && y < 0)
                    PushMouseGesture(UP);
            }

            //画线
            if (IsDrawLine)
            {
                //g.DrawLine(pen, lastPoint.X, lastPoint.Y, xx, yy);
                Program.mainForm.DrawLine(pen, lastPoint.X, lastPoint.Y, xx, yy);
            }

            //不成方向，忽略
            lastPoint.X = xx;
            lastPoint.Y = yy;
        }

        /// <summary>
        /// 根据当次分析手势结果执行动作
        /// </summary>
        /// <returns>是否执行成功</returns>
        public bool ExecuteGesture()
        {
            //System.IO.File.AppendAllText("log.txt", program + ": " + gesture.ToString() + "\r\n", Encoding.UTF8);
            isRecording = false;
            bool isSucceed = false;
            //Program.mainForm.Visible = false;

            //在本程序内画，将传递给动作设置界面
            if (program == Application.ExecutablePath)
            {
                if (gesture.Length > 0)
                {
                    Program.mainForm.SetGesture(gesture.ToString());
                    isSucceed = false;
                }
            }
            else if (gestures.ContainsKey(program))
            {
                string gestureStr = gesture.ToString();
                if (gestures[program].ContainsKey(gestureStr))
                {
                    //gestures[program][gesture].execute();
                    Program.mainForm.ExecuteGesture(gestures[program][gestureStr]);
                    isSucceed = true;
                }
                else
                {
                    //该程序无该动作，执行通用的
                    if (gestures[""].ContainsKey(gestureStr))
                    {
                        //gestures[""][gesture].execute();
                        Program.mainForm.ExecuteGesture(gestures[""][gestureStr]);
                        isSucceed = true;
                    }
                }
            }
            else
            {
                string gestureStr = gesture.ToString();
                //无该程序，执行通用动作 ""
                if (gestures[""].ContainsKey(gestureStr))
                {
                    //gestures[""][gesture].execute();
                    Program.mainForm.ExecuteGesture(gestures[""][gestureStr]);
                    isSucceed = true;
                }
            }
            return isSucceed;
        }

        /// <summary>
        /// 读取手势，填充gestures
        /// </summary>
        /// <param name="fileName">XML文件完整路径</param>
        public bool LoadGestures(string xmlFileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFileName);
                XmlElement root = (XmlElement)(xmlDoc.SelectSingleNode("MyGesture")); //root

                ///////////////////////Exceptions//////////////////////////
                XmlElement exception = (XmlElement)(root.SelectSingleNode("MouseExceptions"));
                foreach (XmlNode fileName in exception.SelectNodes("FileName"))
                {
                    string fileN = fileName.InnerText;
                    Program.mainForm.AddMouseException(fileN);
                }
                /////////////////////////////////////////////////////////////

                ///////////////////////Program//////////////////////////////
                foreach (XmlNode gesture in root.SelectNodes("Program"))
                {
                    XmlElement ges = (XmlElement)gesture;
                    ProgramGesture gestures = new ProgramGesture();

                    foreach (XmlNode ge in ges.SelectNodes("Gesture"))
                    {
                        XmlElement gee = (XmlElement)ge;
                        if (gee.GetAttribute("Type") == "M")
                        {
                            ActionType type = (ActionType)(Enum.Parse(typeof(ActionType), gee.GetAttribute("Action")));

                            if (type == ActionType.KeyPress)
                            {
                                XmlElement key = (XmlElement)(gee.SelectSingleNode("Key"));
                                bool shift = Boolean.Parse(key.GetAttribute("Shift"));
                                bool ctrl = Boolean.Parse(key.GetAttribute("Ctrl"));
                                bool alt = Boolean.Parse(key.GetAttribute("Alt"));
                                bool winKey = Boolean.Parse(key.GetAttribute("WinKey"));
                                gestures.Add(gee.GetAttribute("Name"), new KeyAction((Keys)(Enum.Parse(typeof(Keys), key.InnerText)), shift, ctrl, alt, winKey));
                            }
                            else if (type == ActionType.StartProgram)
                            {
                                XmlElement key = (XmlElement)(gee.SelectSingleNode("FileName"));
                                gestures.Add(gee.GetAttribute("Name"), new StartProAction(key.InnerText));
                            }
                            else if (type == ActionType.MK)
                            {
                                XmlElement key = (XmlElement)(gee.SelectSingleNode("FileName"));
                                gestures.Add(gee.GetAttribute("Name"), new MKAction(key.InnerText, int.Parse(key.GetAttribute("Times")), float.Parse(key.GetAttribute("Speed"))));
                            }
                            else if (type == ActionType.CS)
                            {
                                XmlElement key = (XmlElement)(gee.SelectSingleNode("FileName"));
                                gestures.Add(gee.GetAttribute("Name"), new CSAction(key.InnerText, key.GetAttribute("Args"), key.GetAttribute("Dlls")));
                            }
                            else
                                gestures.Add(gee.GetAttribute("Name"), new MyAction(type));
                        }
                    }
                    if (gestures.Gestures.Count > 0)
                        AddProgramGesture(ges.GetAttribute("FileName"), gestures);
                }
                /////////////////////////////////////////////////////////////
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 恢复默认的手势动作集
        /// </summary>
        public void ResetGestures()
        {
            Program.mainForm.MouseExceptions.Clear();
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            this.gestures.Clear();
            //Program.mainForm.AddMouseException("D:\\Program Files\\Maxthon2\\Maxthon.exe");
            ProgramGesture gestures = new ProgramGesture();

            gestures.Add("↓", new MyAction(ActionType.最小化));
            gestures.Add("↓←", new MyAction(ActionType.静音));
            gestures.Add("↓→", new MyAction(ActionType.关闭));
            gestures.Add("↓↑", new MyAction(ActionType.资源管理器));
            gestures.Add("←", new MyAction(ActionType.后退));
            gestures.Add("→", new MyAction(ActionType.前进));
            gestures.Add("↑", new MyAction(ActionType.最大化));
            gestures.Add("↑↓", new MyAction(ActionType.刷新));
            gestures.Add("↑←", new MyAction(ActionType.上一窗口));
            gestures.Add("↑→", new MyAction(ActionType.下一窗口));
            gestures.Add("→←", new MyAction(ActionType.音量增大));
            gestures.Add("←→", new MyAction(ActionType.音量减小));

            gestures.Add("↓→↑←", new MyAction(ActionType.关机));

            AddProgramGesture("", gestures);

            gestures = new ProgramGesture();

            gestures.Add("←", new MyAction(ActionType.上一首));
            gestures.Add("→", new MyAction(ActionType.下一首));
            gestures.Add("↑↓", new MyAction(ActionType.播放暂停));
            gestures.Add("↓↑", new MyAction(ActionType.播放停止));

            AddProgramGesture(programFiles + "\\Windows Media Player\\wmplayer.exe", gestures);
        }

        /// <summary>
        /// 添加一个程序手势动作
        /// </summary>
        /// <param name="pg"></param>
        public void AddProgramGesture(string program, ProgramGesture pg)
        {
            if (gestures.ContainsKey(program))
                gestures[program] = pg;
            else
                gestures.Add(program, pg);
        }

        /// <summary>
        /// 给特定的程序添加一个手势动作，如果手势已有，那么以前的动作被覆盖
        /// </summary>
        /// <param name="program">程序</param>
        /// <param name="gesture">手势</param>
        /// <param name="action">动作</param>
        public void AddProgramGesture(string program, string gesture, MyAction action)
        {
            if (gestures.ContainsKey(program))
                gestures[program][gesture] = action;
            else
            {
                ProgramGesture pg = new ProgramGesture();
                pg.Add(gesture, action);
                gestures.Add(program, pg);
            }
        }

        /// <summary>
        /// 删除程序动作
        /// </summary>
        /// <param name="program"></param>
        public void RemoveProgramGesture(string program)
        {
            gestures.Remove(program);
        }
    }
}
