using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace MGesture
{
    /// <summary>
    /// 识别一系列按键
    /// </summary>
    public class KeyReco
    {
        //已注册的程序
        private Dictionary<string, ProgramGesture> gestures;

        internal Dictionary<string, ProgramGesture> Gestures
        {
            get { return gestures; }
        }

        //默认使用 ~ 键作为执行键
        private System.Windows.Forms.Keys modiferKey = System.Windows.Forms.Keys.Oemtilde;

        public System.Windows.Forms.Keys ModiferKey
        {
            get { return modiferKey; }
            set { modiferKey = value; }
        }
        /// <summary>
        /// 当次分析的动作
        /// </summary>
        private StringBuilder gesture = new StringBuilder(15);

        public StringBuilder Gesture
        {
            get { return gesture; }
        }

        /// <summary>
        /// 目标程序
        /// </summary>
        private string program;

        /// <summary>
        /// 是否在记录
        /// </summary>
        private bool isRecording;

        public bool IsRecording
        {
            get { return isRecording; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyReco()
        {
            ReInit("");

            gestures = new Dictionary<string, ProgramGesture>();
            isRecording = false;
        }

        /// <summary>
        /// 重初始化，使得对象可以重用
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="program">目标程序</param>
        public void ReInit(string program)
        {
            this.program = program;
            gesture.Remove(0, gesture.Length);
            isRecording = true;
        }

        /// <summary>
        /// 用于单键动作的重初始化
        /// </summary>
        /// <param name="program"></param>
        public void ReInitWithoutRecording(string program)
        {
            this.program = program;
            gesture.Remove(0, gesture.Length);
        }

        /// <summary>
        /// 将一个方向保存下来
        /// </summary>
        /// <param name="direction"></param>
        public bool PushKeyGesture(System.Windows.Forms.Keys key)
        {
            gesture.Append(Enum.GetName(typeof(System.Windows.Forms.Keys), key));
            if (gesture.Length > 10)
            {
                isRecording = false;
                gesture.Remove(0, gesture.Length);
                return false;
            }
            else return true;
        }

        /// <summary>
        /// 保存单次按键动作
        /// </summary>
        /// <param name="gesture"></param>
        public void PushSingleGesture(string gesture)
        {
            this.gesture.Remove(0, this.gesture.Length);
            this.gesture.Append(gesture);
        }

        /// <summary>
        /// 根据当次分析手势结果执行动作
        /// </summary>
        /// <param name="isModifier">指示是否是ctrl shift win 或 alt</param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteGesture(bool isModifier)
        {
            isRecording = false;
            bool isSucceed = false;
            //在本程序内画，将传递给动作设置界面
            //StringBuilder text = new StringBuilder();
            //Win32API.GetWindowText(Win32API.GetForegroundWindow(), text, 20);
            if (program == System.Windows.Forms.Application.ExecutablePath)
            //if (true)
            {
                if (Program.mainForm.IsKeyInputFocused)
                {
                    Program.mainForm.SetGesture(gesture.ToString());
                    if (isModifier)
                        isSucceed = false;
                    else
                        isSucceed = true;
                }
            }
            else if (gestures.ContainsKey(program))
            {
                if (gestures[program].ContainsKey(gesture.ToString()))
                {
                    //gestures[program][gesture].execute();
                    Program.mainForm.ExecuteGesture(gestures[program][gesture.ToString()]);
                    isSucceed = true;
                }
                else
                {
                    //该程序无该动作，执行通用的
                    if (gestures[""].ContainsKey(gesture.ToString()))
                    {
                        //gestures[""][gesture].execute();
                        Program.mainForm.ExecuteGesture(gestures[""][gesture.ToString()]);
                        isSucceed = true;
                    }
                }
            }
            else
            {
                //无该程序，执行通用动作 ""
                if (gestures[""].ContainsKey(gesture.ToString()))
                {
                    //gestures[""][gesture].execute();
                    Program.mainForm.ExecuteGesture(gestures[""][gesture.ToString()]);
                    isSucceed = true;
                }
            }
            return isSucceed;
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
                XmlElement exception = (XmlElement)(root.SelectSingleNode("KeyExceptions"));
                foreach (XmlNode fileName in exception.SelectNodes("FileName"))
                {
                    string fileN = fileName.InnerText;
                    Program.mainForm.AddKeyException(fileN);
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
                        if (gee.GetAttribute("Type") == "K")
                        {
                            ActionType type = (ActionType)(Enum.Parse(typeof(ActionType), gee.GetAttribute("Action")));

                            if (type == ActionType.KeyPress)
                            {
                                XmlElement key = (XmlElement)(gee.SelectSingleNode("Key"));
                                bool shift = Boolean.Parse(key.GetAttribute("Shift"));
                                bool ctrl = Boolean.Parse(key.GetAttribute("Ctrl"));
                                bool alt = Boolean.Parse(key.GetAttribute("Alt"));
                                bool winKey = Boolean.Parse(key.GetAttribute("WinKey"));
                                gestures.Add(gee.GetAttribute("Name"), new KeyAction((System.Windows.Forms.Keys)(Enum.Parse(typeof(System.Windows.Forms.Keys), key.InnerText)), shift, ctrl, alt, winKey));
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
                //System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 恢复默认的手势动作集
        /// </summary>
        public void ResetGestures()
        {
            Program.mainForm.KeyExceptions.Clear();
            this.gestures.Clear();
            //Program.mainForm.AddKeyException("D:\\Program Files\\Maxthon2\\Maxthon.exe");
            ProgramGesture gestures = new ProgramGesture();

            gestures.Add("C", new StartProAction("C:\\Windows\\System32\\calc.exe"));
            gestures.Add("WMP", new StartProAction("C:\\Program Files\\Windows Media Player\\wmplayer.exe"));
            //gestures.Add("D1", new MyAction(ActionType.性能窗口));
            //gestures.Add("#Scroll", new MyAction(ActionType.重设窗口大小));
            gestures.Add("#Pause", new MyAction(ActionType.窗口总在最前));
            //gestures.Add("#Ctrl+Up", new MyAction(ActionType.关闭光驱));
            //gestures.Add("#Ctrl+Down", new MyAction(ActionType.打开光驱));
            gestures.Add("#Alt+Ctrl+L", new MyAction(ActionType.锁定并关闭屏幕));
            gestures.Add("#Ctrl+B", new StartProAction("http://www.baidu.com/s?wd={cp}"));

            AddProgramGesture("", gestures);
        }
    }
}
