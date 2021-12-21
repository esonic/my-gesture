using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MGesture
{
    /// <summary>
    /// 脚本
    /// </summary>
    class CSAction : MyAction
    {
        /// <summary>
        /// 引用
        /// </summary>
        public string Dlls { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Args { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        public CSAction(string fileName, string args, string dlls)
            : base(ActionType.CS)
        {
            FileName = fileName;
            Dlls = dlls;
            Args = args;
        }

        public override void execute()
        {
            CSharpInterpreter.CSharpInterpreter.RunFromSrcFile(FileName, Dlls.Split('|'), Args.Split(' '));
        }

        public override string ToString()
        {
            return "脚本 " + FileName + " 参数 " + Args + " 引用 " + Dlls;
        }
    }
}
