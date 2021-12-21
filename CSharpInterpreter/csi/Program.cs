using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csi
{
    class Program
    {
        static void Main(string[] args)
        {
            bool script = false;
            int argCount = 0;
            string[] arg = null, refs = null;
            string file = null;
            foreach (string ar in args)
            {
                if (ar == "/s")
                    script = true;
                else
                {
                    if (ar.StartsWith("/a"))
                        arg = ar.Substring(2).Split(' ');
                    else if (ar.StartsWith("/r"))
                        refs = ar.Substring(2).Split('|');
                    else file = ar;
                    argCount++;
                }
            }
            if (argCount < 1 || argCount > 3)
            {
                Console.WriteLine("Usage: csi [/s] file [/aargs] [/rrefs]\r\n\r\n/s      Run as script.\r\nargs    Arguments, separated by "
                    + "space.\r\nrefs    Reference assembly, separated by |\r\n\r\neg. csi /s test.cs \"/aarg1 arg2\" \"/rref1.dll|ref2.dll\"");
                return;
            }

            try
            {
                string re = null;
                if (script)
                    re = CSharpInterpreter.CSharpInterpreter.RunFromScriptFile(file, refs, arg);
                else
                    re = CSharpInterpreter.CSharpInterpreter.RunFromSrcFile(file, refs, arg);

                if (re != null)
                    Console.WriteLine("Execute result: " + re);
                else Console.WriteLine("Execute succeed");
            }
            catch (Exception ex)
            {
                string exs = ex.Message;
                while (ex.InnerException != null)
                {
                    exs += "\r\nInner Exception:\r\n";
                    exs += ex.InnerException.Message;
                    ex = ex.InnerException;
                }
                Console.WriteLine("Execute failed: " + exs);
            }
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadLine();
        }
    }
}
