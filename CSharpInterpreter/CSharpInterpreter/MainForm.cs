using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using NRefactory = ICSharpCode.NRefactory;
using Dom = ICSharpCode.SharpDevelop.Dom;
using System.Reflection;

namespace CSharpInterpreter
{
    public partial class MainForm : Form
    {
        internal Dom.ProjectContentRegistry pcRegistry;
        internal Dom.DefaultProjectContent myProjectContent;
        internal Dom.ParseInformation parseInformation;
        Dom.ICompilationUnit lastCompilationUnit;
        System.Threading.Thread parserThread;

        public const string DummyFileName = "edited.cs";

        public MainForm(string file)
        {
            InitializeComponent();

            textEditorControl1.SetHighlighting("C#");
            textEditorControl1.Font = Font;
            textEditorControl1.Font = new System.Drawing.Font(Font.FontFamily, 10);

            textEditorControl1.ActiveTextAreaControl.TextArea.AllowDrop = true;
            textEditorControl1.ActiveTextAreaControl.TextArea.DragEnter += textEditorControl1_DragEnter;
            textEditorControl1.ActiveTextAreaControl.TextArea.DragDrop += textEditorControl1_DragDrop;

            textEditorControl1.Document.FoldingManager.FoldingStrategy = new code_editor.MyFoldStrategy();

            if (file == null || file.Trim().Length == 0)
                ResetCode();
            else
            {
                try
                {
                    textEditorControl1.Text = System.IO.File.ReadAllText(file, Encoding.UTF8);
                }
                catch { }
            }

            HostCallbackImplementation.Register(this);
            CodeCompletionKeyHandler.Attach(this, textEditorControl1);
            ToolTipProvider.Attach(this, textEditorControl1);

            pcRegistry = new Dom.ProjectContentRegistry(); // Default .NET 2.0 registry

            // Persistence lets SharpDevelop.Dom create a cache file on disk so that
            // future starts are faster.
            // It also caches XML documentation files in an on-disk hash table, thus
            // reducing memory usage.
            pcRegistry.ActivatePersistence(Path.Combine(Path.GetTempPath(), "CSharpCodeCompletion"));

            myProjectContent = new Dom.DefaultProjectContent();
            myProjectContent.Language = Dom.LanguageProperties.CSharp;
            // create dummy parseInformation to prevent NullReferenceException when using CC before parsing
            // for the first time
            parseInformation = new Dom.ParseInformation(new Dom.DefaultCompilationUnit(myProjectContent));
        }

        private void button1_Click(object sender, EventArgs ea)
        {
            button1.Enabled = false;
            toolStripMenuItem3.Enabled = false;
            textEditorControl1.IsReadOnly = true;
            parserThreadLabel.Text = "Executing";
            string src = textEditorControl1.Text;
            string[] refs = textBox3.Text.Split('|');
            string[] args = textBox2.Text.Split(' ');
            //execute in a thread
            (new Thread(new ThreadStart(() => {
                try
                {
                    string re = CSharpInterpreter.RunFromSrc(src, refs, args);

                    Invoke(new EventHandler((o, e) =>
                    {
                        parserThreadLabel.Text = "Execute succeed";
                        if (re != null)
                            MessageBox.Show(this, re, "Execute Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        button1.Enabled = true;
                        toolStripMenuItem3.Enabled = true;
                        textEditorControl1.IsReadOnly = false;
                    }));
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
                    Invoke(new EventHandler((o, e) =>
                    {
                        parserThreadLabel.Text = "Execute failed";
                        MessageBox.Show(this, exs, "Execute Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        button1.Enabled = true;
                        toolStripMenuItem3.Enabled = true;
                        textEditorControl1.IsReadOnly = false;
                    }));
                }
            }))).Start();
        }

        private void ResetCode()
        {
            textEditorControl1.Text = "#region\r\nusing System;\r\nusing Common;\r\nusing System.Collections.Generic;\r\nusing System.Drawing;"
                + "\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.IO;\r\nusing System.Threading;\r\nusing System.Windows.Forms;"
                + "\r\n\r\nnamespace CSharpExecutor\r\n{\r\n    public class Executor\r\n    "
                + "{\r\n        public string Execute(string[] args)\r\n        {object re=null;\r\n#endregion\r\n\r\n//Insert your code here" //insert point
                + "\r\n\r\n#region\r\n        if(re!=null)return re.ToString();else return null;}\r\n    }\r\n}\r\n#endregion\r\n";

            textEditorControl1.ActiveTextAreaControl.SelectionManager.SetSelection(
                new ICSharpCode.TextEditor.TextLocation(0, 19), new ICSharpCode.TextEditor.TextLocation(23, 19));
            textEditorControl1.ActiveTextAreaControl.Caret.Line = 19;
            textEditorControl1.ActiveTextAreaControl.Caret.Column = 23;
            textEditorControl1.ActiveTextAreaControl.ScrollTo(0);

            textEditorControl1.Invalidate();
            textEditorControl1.ActiveTextAreaControl.Invalidate();
            textEditorControl1.ActiveTextAreaControl.TextArea.Invalidate();
            textEditorControl1.Document.FoldingManager.UpdateFoldings(null, null);
            textEditorControl1.Document.FoldingManager.GetFoldingsWithStart(0)[0].IsFolded = true;
            textEditorControl1.Document.FoldingManager.GetFoldingsWithStart(21)[0].IsFolded = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WinHotKey.SetHotKey(false, false, false, false, Keys.F10, Handle, 99);

            parserThread = new Thread(ParserThread);
            parserThread.IsBackground = true;
            parserThread.Start();

            //ResetCode();
            textEditorControl1.TextChanged += (s, ev) => {
                textEditorControl1.Document.FoldingManager.UpdateFoldings(null, null);
            };
            
            #region fill common actions list
            //Assembly asm = Assembly.LoadFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Common.dll");
            //Type actions = asm.GetType("Common.Actions");
            //MethodInfo[] methods = actions.GetMethods();
            //foreach (MethodInfo m in methods)
            //{
            //    if (!m.IsVirtual && m.Name != "GetType")
            //    {
            //        string decl = m.ReturnType.Name + " " + m.Name + "(";
            //        foreach (ParameterInfo p in m.GetParameters())
            //            decl += p.ParameterType.Name + ", ";
            //        if (decl.EndsWith(", ")) decl = decl.Substring(0, decl.Length - 2);
            //        ToolStripMenuItem item = new System.Windows.Forms.ToolStripMenuItem(decl + ")");
            //        item.Click += (s, ev) =>
            //        {
            //            ToolStripMenuItem i = s as ToolStripMenuItem;
            //            textEditorControl1.ActiveTextAreaControl.TextArea.InsertString("Actions." + i.Text.Substring(i.Text.IndexOf(' ') + 1));
            //        };
            //        this.insertActionsToolStripMenuItem.DropDownItems.Add(item);
            //    }
            //}
            #endregion

            try
            {
                //fill code snippet
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\code snippet";
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    ToolStripMenuItem item = new System.Windows.Forms.ToolStripMenuItem(file.Substring(file.LastIndexOf('\\') + 1, file.Length - file.LastIndexOf('\\') - 1));
                    item.Click += (s, ev) =>
                    {
                        try
                        {
                            ToolStripMenuItem i = s as ToolStripMenuItem;
                            string text = File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\code snippet\\" + i.Text);
                            textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(text);
                        }
                        catch { MessageBox.Show(this, "Load code snippet file failed", "CSharp Interpreter", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    };
                    this.codeToolItem.DropDownItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Load code snippet files failed:\r\n" + ex.Message, "CSharp Interpreter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string configFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.ini";
            //读取配置文件
            if (System.IO.File.Exists(configFile))
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(configFile);
                    if (Int32.Parse(lines[0]) == 0)
                        splitter1_Click(null, null);
                }
                catch
                {
                    //MessageBox.Show(this, "读取配置文件失败\r\n" + ex.Message, "Moe Loader", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    int id = Int32.Parse(m.WParam.ToString());
                    if (id == 99)
                    {
                        //exe
                        button1_Click(null, null);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        void ParserThread()
        {
            BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading mscorlib..."; }));
            myProjectContent.AddReferencedContent(pcRegistry.Mscorlib);

            // do one initial parser step to enable code-completion while other
            // references are loading
            ParseStep();

            string[] referencedAssemblies = {
				"System", "System.Data", "System.Drawing", "System.Xml", "System.Windows.Forms"
			};
            foreach (string assemblyName in referencedAssemblies)
            {
                string assemblyNameCopy = assemblyName; // copy for anonymous method
                BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading " + assemblyNameCopy + "..."; }));
                Dom.IProjectContent referenceProjectContent = pcRegistry.GetProjectContentForReference(assemblyName, assemblyName);
                myProjectContent.AddReferencedContent(referenceProjectContent);
                if (referenceProjectContent is Dom.ReflectionProjectContent)
                {
                    (referenceProjectContent as Dom.ReflectionProjectContent).InitializeReferences();
                }
            }
            string cpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Common.dll";
            Dom.IProjectContent cp = pcRegistry.GetProjectContentForReference("Common", cpath);
            myProjectContent.AddReferencedContent(cp);

            BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Ready"; }));

            // Parse the current file every 2 seconds
            while (!IsDisposed)
            {
                ParseStep();

                Thread.Sleep(2000);
            }
        }

        void ParseStep()
        {
            string code = null;
            Invoke(new MethodInvoker(delegate
            {
                try
                {
                    code = textEditorControl1.Text;
                }
                catch { }
            }));
            TextReader textReader = new StringReader(code);
            Dom.ICompilationUnit newCompilationUnit;
            NRefactory.SupportedLanguage supportedLanguage;

            supportedLanguage = NRefactory.SupportedLanguage.CSharp;
            using (NRefactory.IParser p = NRefactory.ParserFactory.CreateParser(supportedLanguage, textReader))
            {
                // we only need to parse types and method definitions, no method bodies
                // so speed up the parser and make it more resistent to syntax
                // errors in methods
                p.ParseMethodBodies = false;

                p.Parse();
                newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
            }
            // Remove information from lastCompilationUnit and add information from newCompilationUnit.
            myProjectContent.UpdateCompilationUnit(lastCompilationUnit, newCompilationUnit, DummyFileName);
            lastCompilationUnit = newCompilationUnit;
            parseInformation = new Dom.ParseInformation(newCompilationUnit);
        }

        Dom.ICompilationUnit ConvertCompilationUnit(NRefactory.Ast.CompilationUnit cu)
        {
            Dom.NRefactoryResolver.NRefactoryASTConvertVisitor converter;
            converter = new Dom.NRefactoryResolver.NRefactoryASTConvertVisitor(myProjectContent);
            cu.AcceptVisitor(converter, null);
            return converter.Cu;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, textEditorControl1.Text);
            }
        }

        private void textEditorControl1_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files[0].EndsWith("cs", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else e.Effect = DragDropEffects.None;
            }
            catch { e.Effect = DragDropEffects.None; }
        }

        private void textEditorControl1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files[0].EndsWith("cs", StringComparison.OrdinalIgnoreCase))
                {
                    textEditorControl1.Text = File.ReadAllText(files[0]);
                }
                //else e.Effect = DragDropEffects.None;
            }
            catch { //e.Effect = DragDropEffects.None;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                textEditorControl1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ResetCode();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedText = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (selectedText != null && selectedText.Length > 0)
            {
                Clipboard.SetText(selectedText);
                textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedText = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (selectedText != null && selectedText.Length > 0)
            {
                Clipboard.SetText(selectedText);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = Clipboard.GetText();
            if (text != null)
                textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(text);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string selectedText = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (selectedText != null && selectedText.Length > 0)
            {
                copyToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch { }
        }

        private void splitter1_Click(object sender, EventArgs e)
        {
            const int margin = 8;
            const int pheight = 74;
            if (splitter1.MinSize == 0)
            {
                splitter1.MinSize = pheight;
                panel1.Height = pheight;
                textEditorControl1.Height = ClientSize.Height - statusStrip1.Height - margin - pheight;
                textEditorControl1.Location = new Point(0, pheight + margin);
            }
            else
            {
                splitter1.MinSize = 0;
                panel1.Height = 0;
                textEditorControl1.Height = ClientSize.Height - statusStrip1.Height - margin;
                textEditorControl1.Location = new Point(0, margin);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
