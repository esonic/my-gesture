using System;
using System.Collections.Generic;
using System.Text;

namespace MGesture
{
    public class ProgramGesture
    {
        //已注册的手势、动作集
        private Dictionary<string, MyAction> gestures;

        public Dictionary<string, MyAction> Gestures
        {
            get { return gestures; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProgramGesture()
        {
            gestures = new Dictionary<string, MyAction>();
        }

        public bool ContainsKey(string gesture)
        {
            return gestures.ContainsKey(gesture);
        }

        /// <summary>
        /// 设置手势动作
        /// </summary>
        /// <param name="gesture"></param>
        /// <returns></returns>
        public MyAction this[string gesture]
        {
            get 
            {
                if (gestures.ContainsKey(gesture))
                    return gestures[gesture];
                else return null;
            }
            set
            {
                Add(gesture, value);
            }
        }

        /// <summary>
        /// 添加一个手势动作，如果手势已有，那么以前的动作被覆盖
        /// </summary>
        /// <param name="gesture">手势</param>
        /// <param name="action">动作</param>
        public void Add(string gesture, MyAction action)
        {
            if (gestures.ContainsKey(gesture))
                gestures[gesture] = action;
            else
                gestures.Add(gesture, action);
        }

        /// <summary>
        /// 删除手势动作
        /// </summary>
        /// <param name="gesture"></param>
        public void Remove(string gesture)
        {
            gestures.Remove(gesture);
        }
    }
}
