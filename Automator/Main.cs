using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsInput;
using System.Diagnostics;
using System.Linq;

namespace Automator
{

    public partial class AutomatorForm : Form
    {
        private string tab = "  ";
        public System.Windows.Forms.Timer simulation = null;
        private System.Windows.Forms.Timer mouseUpdater = null;
        public static Stopwatch stopwatch = null;
        private string CCode = "";
        private string codeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Automator";
        private string codeFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Automator\\temp";
        private List<string> commands;
        private object compilerInstance = null;
        private string[] descriptions = new string[] {
            "using","using - provides a convenient syntax that ensures the correct use of IDisposable objects",
            "class","class - defines the kinds of data and the functionality their objects will have",
            "main","main - specifies where to begin executing the code",
            "Compiler","Compiler - the section of code targetted by the compiler",
            "public","public - an access modifier for types and type members",
            "private","private - an access modifier for types and type members",
            "protected","protected - an access modifier for types and type members",
            "internal","internal - an access modifier for types and type members",
            "for","for - run a statement or a block of statements repeatedly until a specified expression evaluates to false",
            "foreach","foreach - repeats a group of embedded statements for each element in an array",
            "while","while - executes a statement or a block of statements until a specified expression evaluates to false",
            "do","do - executes a statement or a block of statements repeatedly until a specified expression evaluates to false",
            "namespace","namespace - used to declare a scope that contains a set of related objects",
            "if","if - identifies which statement to run based on the value of a Boolean expression",
            "switch","switch - a selection statement that chooses a single switch section to execute from a list of candidates based on a pattern match",
            "null","null - a literal that represents a null reference, one that does not refer to any object",
            "int","int - denotes an integral type",
            "string","string - represents a sequence of zero or more Unicode characters",
            "double","double - signifies a simple type that stores 64-bit floating-point values",
            "float","float - a simple type that stores 32-bit floating-point values",
            "bool","bool - an alias of System.Boolean. It is used to declare variables to store the Boolean values, true and false",
            "throw","throw - signals the occurrence of an exception during program execution",
            "short","short - denotes an integral data type that stores values from -32,768 to 32,767",
            "var","var - the compiler determines the type",
            "void","void - specifies that the method doesn't return a value",
            "virtual","virtual - used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class",
            "byte","byte - denotes an integral type that stores values from 0 to 255",
            "new","new - can be used as an operator, a modifier, or a constraint",
            "get","get - defines an accessor method in a property or indexer that returns the property value or the indexer element",
            "set","set - defines an accessor method in a property or indexer that assigns a value to the property or the indexer element",
            "continue","continue - the statements between continue and the end of the for body are skipped",
            "break","break - terminates the closest enclosing loop or switch statement in which it appears",
            "","", //Start of custom functions
            "ElapsedMilliseconds","ElapsedMilliseconds() - returns number of milliseconds since simulation began",
            "ElapsedSeconds","ElapsedSeconds() - returns number of seconds since simulation began",
            "ElapsedMinutes","ElapsedMinutes() - returns number of minutes since simulation began",
            "ElapsedHours","ElapsedHours() - returns number of hours since simulation began",
            "Alert","Alert(message) - display a message box to the user",
            "Prompt","Prompt(message [, title]) - get a string input from the user",
            "Sleep","Sleep(milliseconds) - pause execution for a certain amount of time",
            "RandomF","RandomF([a,] b) - generate a random double from a to b",
            "Random","Random([a,] b) - generate a random integer from a to b",
            "IsKeyDown","IsKeyDown(key) - returns true if key is currently pressed down",
            "PressKey","PressKey(key, seconds) - presses a key for a certain amount of time",
            "KeyDown","KeyDown(key) - holds down a key",
            "KeyUp","KeyUp(key) - releases a key",
            "FastType","FastType(string message) - send instant keystrokes to type a message",
            "RealisticType","FastType(string message) - send keystrokes to type a message with pauses",
            "MoveMouse","MoveMouse(x, y [, width, height]) - animate the mouse moving to a portion of the screen",
            "SetCursor","SetCursor(x, y) - set the cursor position instantly",
            "GetCursorX","GetCursorX() - get the cursor's x-position",
            "GetCursorY","GetCursorY() - get the cursor's y-position",
            "RightClick","RightClick() - send a right click keystroke",
            "RMouseDown","RMouseDown() - holds down right mouse button",
            "RMouseUp","RMouseUp() - releases right mouse button",
            "MiddleClick","MiddleClick() - send a middle click keystroke",
            "MiddleDown","MiddleDown() - holds down middle mouse button",
            "MiddleUp","MiddleUp() - releases middle mouse button",
            "Click","Click() - send a left click keystroke",
            "MouseDown","MouseDown() - holds down left mouse button",
            "MouseUp","MouseUp() - releases left mouse button",
            "ScreenWidth","ScreenWidth() - returns the screens width",
            "ScreenHeight","ScreenHeight() - returns the screens height",
            "GetColorAt","GetColorAt(x, y) - returns a (R, G, B) color at screen point",
            "Break","Break([message]) - Stop the program"
        };

        private string[] presets = new string[] {
            "Multitasking using threading","using System.Threading;","Thread thread = new Thread(() => {\n//Code goes here\n});\nthread.Start();"
        };

        public AutomatorForm()
        {
            InitializeComponent();
        }
        
        private void AutomatorForm_Load(object sender, EventArgs e)
        {
            instructions.AutoWordSelection = false;
            fetchFiles();

            ((ToolStripDropDownMenu)presetsMenuItem.DropDown).ShowImageMargin = false;
            for(int i=0;i<presets.Length;i+=3)
            {
                string title = presets[i], include = presets[i + 1], code = presets[i + 2];
                var item = presetsMenuItem.DropDownItems.Add(title);
                item.BackColor = Color.Teal;
                item.ForeColor = Color.White;
                item.Tag = include;
                item.ToolTipText = code;
            }

            ((ToolStripDropDownMenu)FunctionsMenuItem.DropDown).ShowImageMargin = false;
            commands = new List<string>();
            MethodInfo[] methods = typeof(Functions).GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.Name == "_eof") break; //skip functions like toString();
                commands.Add(method.Name);
                var name = method.Name + "(";
                ParameterInfo[] parameters = method.GetParameters();
                foreach(ParameterInfo parameter in parameters)
                {
                    name += formatType(parameter.ParameterType.Name) + " " + parameter.Name + ", ";
                }
                if (parameters.Length > 0) name = name.Remove(name.Length - 2); //get rid of extra comma
                name += ")";
                var item = FunctionsMenuItem.DropDownItems.Add(formatType(method.ReturnType.Name) + " " + name);
                item.BackColor = Color.Teal;
                item.ForeColor = Color.White;
                item.ToolTipText = name;
            }
            commands = commands.ToArray().Distinct().ToList<string>(); //remove duplicates
            load();
            FormatCode();
        }

        private void AutomatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void presetsMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string include = (string)e.ClickedItem.Tag, code = e.ClickedItem.ToolTipText;
            int i = 0;
            string[] lines = instructions.Lines;
            bool alreadyExists = false;
            while (i + 1 < lines.Length && (lines[i].StartsWith("//") || lines[i] == "" || lines[i].StartsWith("using ")))
            {
                i++;
                if (lines[i].Trim() == include.Trim()) alreadyExists = true;
            }
            if (!alreadyExists)
            {
                while (i > 0 && lines[i - 1].Trim() == "") i--;
                lines[i] = include + "\n" + lines[i];
                instructions.Text = String.Join("\n", lines);
            }
            i = instructions.Text.Length + 1;
            instructions.Text += "\n" + code + "\n";
            instructions.Focus();
            instructions.SelectionStart = i - 1;
            FormatCode();
        }

        private string formatType(string type)
        {
            return type.Replace("Int32", "Int").Replace("Boolean","Bool").ToLower();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Functions.Prompt("Enter file name:","Save Dialog");
            if (name == "") return;
            if (!Directory.Exists(codeDirectory)) Directory.CreateDirectory(codeDirectory);
            File.WriteAllText(codeDirectory + "\\" + name + ".cs", instructions.Text);
            fetchFiles();
        }

        private void loadFileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (File.Exists(e.ClickedItem.ToolTipText)) instructions.Text = File.ReadAllText(e.ClickedItem.ToolTipText);
        }


        private void openFineLocation_Click(object sender, EventArgs e)
        {
            Process.Start(codeDirectory);
        }

        private void fetchFiles() {
            loadFileToolStripMenuItem.DropDownItems.Clear();
            ((ToolStripDropDownMenu)loadFileToolStripMenuItem.DropDown).ShowImageMargin = false;
            loadFileToolStripMenuItem.Enabled = true;
            string[] files = Directory.GetFiles(codeDirectory);
            foreach (string file in files)
            {
                if (Path.GetExtension(file) != ".cs") continue;
                string name = Path.GetFileName(file);
                var item = loadFileToolStripMenuItem.DropDownItems.Add(name);
                item.BackColor = Color.Teal;
                item.ForeColor = Color.White;
                item.ToolTipText = file;
            }
            if (!loadFileToolStripMenuItem.HasDropDownItems) loadFileToolStripMenuItem.Enabled = false;
        }

        private void KeysMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            instructions.Focus();
            instructions.AppendText("\npressKey(" + e.ClickedItem.Text + ");");

            int start = instructions.GetFirstCharIndexFromLine(instructions.Lines.Length);
            if (start < 0) return;
            int length = instructions.Lines[instructions.Lines.Length - 1].Length;
            instructions.Select(start, length);
        }

        private void FunctionsMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            instructions.Focus();
            instructions.AppendText("\n"+e.ClickedItem.ToolTipText);

            int start = instructions.GetFirstCharIndexFromLine(instructions.Lines.Length);
            if (start < 0) return;
            int length = instructions.Lines[instructions.Lines.Length - 1].Length;
            instructions.Select(start, length);
        }

        private void save()
        {
            if (!Directory.Exists(codeDirectory)) Directory.CreateDirectory(codeDirectory);
            File.WriteAllText(codeFilePath, instructions.Text);
        }

        private void load()
        {
            if(File.Exists(codeFilePath)) instructions.Text = File.ReadAllText(codeFilePath);
        }


        private void loadLastSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }

        private void menuContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loadLastSaveToolStripMenuItem.Visible = File.Exists(codeFilePath);
        }

        private void loadDefaultProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(codeFilePath);
            Application.Restart();
        }

        private void toggleBtn_Click(object sender, EventArgs e)
        {
            if (simulation!=null)
            {
                stopSimulation();
                Application.Restart();
            }
            else
            {
                startSimulation();
            }
        }

        private void startSimulation()
        {
            this.ActiveControl = null;
            try
            {
                if (instructions.Text.StartsWith("//Interval:")) intervalSeconds.Value = decimal.Parse(instructions.Lines[0].Substring(11).Trim());
            }
            catch (Exception e) {
                MessageBox.Show("The interval value given is invalid.\n"+ e.Message, "Error");
                return;
            }
                try
            {
                CCode = toCSharp(instructions.Text);
                save();
                simulation = new System.Windows.Forms.Timer();
                if (intervalSeconds.Value > 0)
                {
                    simulation.Tick += new EventHandler(step);
                    simulation.Interval = Decimal.ToInt32(1000 * intervalSeconds.Value);
                    simulation.Start();
                }
                else step(null, null);
                toggleBtn.Text = "Stop Simulation";
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            catch (Exception e)
            {
                stopSimulation();
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void stopSimulation()
        {
            if (simulation == null) return;
            simulation.Stop();
            simulation.Dispose();
            simulation = null;
            compilerInstance = null;
            toggleBtn.Invoke((MethodInvoker)delegate
            {
                toggleBtn.Text = "Start Simulation";
            });
            if (stopwatch != null)
            {
                stopwatch.Stop();
                stopwatch = null;
            }
        }

        private string toCSharp(string code)
        {
            List<string> patterns = new List<string>();
            List<string> replacements = new List<string>();
            foreach(string cmd in commands)
            {
                patterns.Add(cmd);
                replacements.Add("Automator.Functions." + cmd);
            }

            code = SafeReplace(code, patterns.ToArray(), replacements.ToArray(), false);

            //MessageBox.Show(code,"Finalized Code");
            return code;
        }

        //Replace that skips comments and quotes
        private string SafeReplace(string str, string[] patterns, string[] replacements, bool ignoreCase)
        {
            List<string> quotes = new List<string>();
            Regex rgx1 = new Regex("([\"])(?:\\\\?[\\s\\S])*?\\1", ignoreCase?RegexOptions.IgnoreCase:RegexOptions.None);
            foreach (Match match in rgx1.Matches(str))
                quotes.Add(match.Value);
            List<string> comments = new List<string>();
            Regex rgx2 = new Regex("\\\\[^\\n]+\\n", RegexOptions.IgnoreCase);
            foreach (Match match in rgx2.Matches(str))
                comments.Add(match.Value);
            str = rgx1.Replace(str, "\u3145");
            str = rgx2.Replace(str, "\u3144");

            if (patterns.Length > 1)
            {
                for (int i = 0; i < patterns.Length; i++)
                {
                    str = replace(str, "\\b" + patterns[i] + "\\b", replacements[i]);
                }
            } else
            {
                str = replace(str, patterns[0], replacements[0]);
            }

            foreach (string quote in quotes)
                str = replaceFirst(str, "\u3145", quote);
            foreach (string comment in comments)
                str = replaceFirst(str, "\u3144", comment);
            return str;
        }

        //Count that skips comments and quotes
        private int SafeCount(string str, string pattern)
        {
            List<string> quotes = new List<string>();
            Regex rgx1 = new Regex("([\"])(?:\\\\?[\\s\\S])*?\\1", RegexOptions.IgnoreCase);
            foreach (Match match in rgx1.Matches(str))
                quotes.Add(match.Value);
            List<string> comments = new List<string>();
            Regex rgx2 = new Regex("\\\\[^\\n]+\\n", RegexOptions.IgnoreCase);
            foreach (Match match in rgx2.Matches(str))
                comments.Add(match.Value);
            str = rgx1.Replace(str, "\u3145");
            str = rgx2.Replace(str, "\u3144");
            return Regex.Matches(str, pattern).Count;
        }

        private string replace(string str, string pattern, string replacement)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            return rgx.Replace(str, replacement);
        }

        private int count(string str, string pattern)
        {
            return Regex.Matches(str, pattern).Count;
        }

        private string replaceFirst(string str, string pattern, string replacement)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            return rgx.Replace(str, replacement, 1);
        }

        private void step(object sender, EventArgs e)
        {
            if (simulation == null) return;
            Thread thread = new Thread(() => {
                Eval(CCode);
                if (intervalSeconds.Value == 0) stopSimulation();
            });
            thread.Start();
        }

        private object Eval(string sExpression)
        {
            if (compilerInstance == null)
            {
                CSharpCodeProvider c = new CSharpCodeProvider();
                CompilerParameters cp = new CompilerParameters();
                cp.ReferencedAssemblies.Add("System.Data.dll");
                cp.ReferencedAssemblies.Add("System.Xml.dll");
                cp.ReferencedAssemblies.Add("mscorlib.dll");
                cp.ReferencedAssemblies.Add("InputSimulator.dll");
                ///////////////////////////////////////////////////////////////////
                //cp.ReferencedAssemblies.Add(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("InputSimulator.dll").ToString());
                //cp.ReferencedAssemblies.Add("Properties.Resources.InputSimulator");
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                cp.ReferencedAssemblies.Add(executingAssembly.Location);
                foreach (AssemblyName assemblyName in executingAssembly.GetReferencedAssemblies())
                {
                    cp.ReferencedAssemblies.Add(Assembly.Load(assemblyName).Location);
                }
                cp.CompilerOptions = "/optimize";
                cp.GenerateExecutable = false;
                cp.GenerateInMemory = true;
                StringBuilder sb = new StringBuilder(sExpression + " \n");
                CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
                if (cr.Errors.Count > 0)
                {
                    stopSimulation();

                    /*int i = 0;
                    string lineNumberedCode = "";
                    string[] lines = sb.ToString().Split(new char[] { '\n' }, StringSplitOptions.None);
                    foreach (string ln in lines)
                    {
                        i++;
                        lineNumberedCode += i;
                        if (cr.Errors[0].Line == i) lineNumberedCode += " *   ";
                        else lineNumberedCode += "     ";
                        lineNumberedCode += ln + "\n";
                    }
                    MessageBox.Show(cr.Errors[0].ErrorText + "\n\nCode:\n" + lineNumberedCode, "Runtime Error - Line " + cr.Errors[0].Line, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    */
                    int startOfCode = SafeCount(sb.ToString().Substring(0, 0), "\n") + 1;
                    int line = cr.Errors[0].Line - startOfCode, column = cr.Errors[0].Column;
                    if (line < 1) line = 1;
                    instructions.Invoke((MethodInvoker)delegate
                    {
                        int start = instructions.GetFirstCharIndexFromLine(line);
                        instructions.Focus();
                        instructions.Select(start, instructions.Lines[line].Length);
                        MessageBox.Show(cr.Errors[0].ErrorText + "\n\nLine " + (line + 1), "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    return null;
                }

                System.Reflection.Assembly a = cr.CompiledAssembly;
                try
                {
                    compilerInstance = a.CreateInstance("Compiler.Compiler");
                }
                catch (Exception e)
                {
                    stopSimulation();
                    MessageBox.Show(e.Message, "Error");
                    return null;
                }
            }
            return attemptExecute("main", true);
        }

        private object attemptExecute(string function, bool error)
        {
            try
            {
                Type t = compilerInstance.GetType();
                MethodInfo mi = t.GetMethod("main");
                object s = mi.Invoke(compilerInstance, null);
                return s;
            }
            catch (Exception e)
            {
                if (error)
                {
                    stopSimulation();
                    MessageBox.Show(e.Message, "Error");
                }
            }
            return null;
        }

        private void instructions_TextChanged(object sender, EventArgs e)
        {
            stopSimulation();
        }
        
        private void intervalSeconds_ValueChanged(object sender, EventArgs e)
        {
            stopSimulation();
        }

        private void instructions_Enter(object sender, EventArgs e)
        {
            stopSimulation();
        }

        private void instructions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) FormatCode(); //Enter key
            if (e.KeyChar == '}') FormatCode();
        }

        private void instructions_Leave(object sender, EventArgs e)
        {
            FormatCode();
        }

        private void FormatCode()
        {
            int sline, sstart = instructions.SelectionStart, slength = instructions.SelectionLength;
            string code = instructions.Text;
            sline = count(code.Substring(0, sstart), "\n");
            sstart -= SafeCount(code.Substring(0, sstart), tab) * tab.Length; //update selection with tabs
            code = SafeReplace(code, new[] { tab }, new[] { "" }, true);
            string[] lines = code.Split(new[] { "\n" }, StringSplitOptions.None);
            int depth = 0, trackLength = 0, soffset = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                depth -= SafeCount(lines[i], "}");
                if (depth < 0) depth = 0;
                if (trackLength < sstart) soffset += depth * tab.Length; //update selection with tabs
                trackLength += lines[i].Length + 1;
                for (int j = 0; j < depth; j++)
                {
                    lines[i] = tab + lines[i];
                }
                depth += SafeCount(lines[i], "{");
                if (i == sline) soffset += depth*tab.Length;

            }
            code = String.Join("\n", lines);
            instructions.Text = code;
            instructions.SelectionStart = sstart + soffset;
            instructions.SelectionLength = slength;
        }

        private void instructions_SelectionChanged(object sender, EventArgs e)
        {
            string code = instructions.Text;
            int ss = instructions.SelectionStart;
            if (ss == code.Length || !(Char.IsLetter(code[ss]) || code[ss] == '_')) ss--; //evenly determine what side to look at
            if (code.Length>0 && ss >= 0)
            {
                int a = ss, b = ss;
                while (a > 0 && (Char.IsLetterOrDigit(code[a - 1]) || code[a - 1] == '_')) a--;
                while (b < code.Length && (Char.IsLetterOrDigit(code[b]) || code[b] == '_')) b++;
                if (Char.IsDigit(code[a])) a++;
                string word = code.Substring(a, b - a);
                for (int i = 0; i < descriptions.Length / 2; i++)
                    if (descriptions[i * 2] == word)
                    {
                        word = descriptions[i * 2 + 1];
                        break;
                    }
                status.Text = word;
                status.ToolTipText = word;
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            menuContext.Show(screenRectangle.Left, screenRectangle.Top + controlsLayoutPanel.Height);
        }

        private void samplerFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer sampler = new Timer();
            sampler.Show();
        }

        private void mouseLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MouseLogger sampler = new MouseLogger();
            sampler.Show();
        }

        private void AutomatorForm_Activated(object sender, EventArgs e)
        {
            mouseUpdater = new System.Windows.Forms.Timer();
            mouseUpdater.Tick += new EventHandler(stepMouseInfo);
            mouseUpdater.Interval = Decimal.ToInt32(300);
            mouseUpdater.Start();
            stepMouseInfo(null, null);
            fetchFiles();
        }

        private void AutomatorForm_Deactivate(object sender, EventArgs e)
        {
            mouseUpdater.Stop();
            mouseUpdater.Dispose();
        }

        private void stepMouseInfo(object sender, EventArgs e) {
            mouseLbl.Text = "Mouse: (" + Cursor.Position.X + "," + Cursor.Position.Y + ")";
            Color c = Functions.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
            colorLbl.Text = "Color: (" + c.R + "," + c.G + "," + c.B + ")";
        }
    }




    public static class Functions
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSE_LEFTDOWN = 0x02;
        private const int MOUSE_LEFTUP = 0x04;
        private const int MOUSE_RIGHTDOWN = 0x08;
        private const int MOUSE_RIGHTUP = 0x10;
        private const int MOUSE_MIDDLEDOWN = 0x20;
        private const int MOUSE_MIDDLEUP = 0x40;
        private static Random myRandom = new Random();


        public static double RandomF(double a, double b)
        {
            return myRandom.NextDouble() * (b - a) + a;
        }

        public static double RandomF(double b)
        {
            return myRandom.NextDouble() * b;
        }

        public static int Random(int a, int b)
        {
            return myRandom.Next(a, b + 1);
        }

        public static int Random(int b)
        {
            return myRandom.Next(0, b + 1);
        }

        public static void Alert(object msg)
        {
            MessageBox.Show(msg.ToString());
        }

        public static int ElapsedMilliseconds()
        {
            return (int)AutomatorForm.stopwatch.ElapsedMilliseconds;
        }

        public static int ElapsedSeconds()
        {
            return (int)AutomatorForm.stopwatch.ElapsedMilliseconds / 1000;
        }

        public static int ElapsedMinutes()
        {
            return (int)AutomatorForm.stopwatch.ElapsedMilliseconds / 60000;
        }

        public static int ElapsedHours()
        {
            return (int)AutomatorForm.stopwatch.ElapsedMilliseconds / 3600000;
        }

        public static string Prompt(object text)
        {
            return Prompt(text, "");
        }

        public static string Prompt(object text, object title)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AutomatorForm));
            Form prompt = new Form()
            {
                Width = 300,
                Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title.ToString(),
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.FromArgb(0, 64, 64),
                ForeColor = Color.White,
                Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon")
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Text = text.ToString() };
            TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 265 };
            Button confirmation = new Button() { Text = "Ok", Left = 205, Width = 70, Top = 60, DialogResult = DialogResult.OK, BackColor = Color.WhiteSmoke, ForeColor = Color.Black };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static void Sleep(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        public static void Sleep(int millisecondsFrom, int millisecondsTo)
        {
            System.Threading.Thread.Sleep(Random(millisecondsFrom, millisecondsTo));
        }

        public static bool IsKeyDown(string key)
        {
            VirtualKeyCode keyCode = getKeyCode(key);
            if (keyCode == 0) return false;
            return InputSimulator.IsKeyDown(keyCode);
        }

        public static void PressKey(string key)
        {
            PressKey(key, Random(60, 100));
        }

        public static void PressKey(string key, int milliseconds)
        {
            VirtualKeyCode keyCode = getKeyCode(key);
            if (keyCode == 0) return;
            InputSimulator.SimulateKeyDown(keyCode);
            Sleep(milliseconds);
            InputSimulator.SimulateKeyUp(keyCode);
        }

        public static void KeyDown(string key)
        {
            VirtualKeyCode keyCode = getKeyCode(key);
            if (keyCode == 0) return;
            InputSimulator.SimulateKeyDown(keyCode);
            Sleep(60, 300);
        }

        public static void KeyUp(string key)
        {
            VirtualKeyCode keyCode = getKeyCode(key);
            if (keyCode == 0) return;
            InputSimulator.SimulateKeyUp(keyCode);
            Sleep(60, 300);
        }

        public static void FastType(object msg)
        {
            InputSimulator.SimulateTextEntry(msg.ToString());
        }

        public static void RealisticType(object msg) {
            foreach(char c in msg.ToString())
            {
                Sleep(60, 200);
                VirtualKeyCode key = getKeyCode(c.ToString());
                if (key == 0) continue;
                if (Char.IsUpper(c))
                {
                    if (!InputSimulator.IsKeyDown(VirtualKeyCode.SHIFT))
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.SHIFT);
                        Sleep(60, 300);
                    }
                    InputSimulator.SimulateKeyDown(key);
                    Sleep(60, 300);
                    InputSimulator.SimulateKeyUp(key);
                }
                else
                {
                    if (InputSimulator.IsKeyDown(VirtualKeyCode.SHIFT))
                    {
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.SHIFT);
                        Sleep(60, 300);
                    }
                    InputSimulator.SimulateKeyDown(key);
                    Sleep(60, 300);
                    InputSimulator.SimulateKeyUp(key);
                }
            }
        }
        
        private static VirtualKeyCode getKeyCode(string c) {
            c = c.ToLower();
            if (c == "shift") return VirtualKeyCode.SHIFT;
            if (c == "enter") return VirtualKeyCode.RETURN;
            if (c == "up") return VirtualKeyCode.UP;
            if (c == "down") return VirtualKeyCode.DOWN;
            if (c == "left") return VirtualKeyCode.LEFT;
            if (c == "right") return VirtualKeyCode.RIGHT;
            if (c == " ") return VirtualKeyCode.SPACE;
            if (c == ",") return VirtualKeyCode.OEM_COMMA;
            if (c == "tab") return VirtualKeyCode.TAB;
            if (c == "leftclick") return VirtualKeyCode.LBUTTON;
            if (c == "rightclick") return VirtualKeyCode.RBUTTON;
            if (c == "middlebutton") return VirtualKeyCode.MBUTTON;
            if (c == "scroll") return VirtualKeyCode.SCROLL;
            if (c == "ctrl") return VirtualKeyCode.CONTROL;
            if (c == "esc") return VirtualKeyCode.ESCAPE;
            if (c == "rshift") return VirtualKeyCode.RSHIFT;
            if (c == "rctrl") return VirtualKeyCode.RCONTROL;
            if (c == "a") return VirtualKeyCode.VK_A;
            if (c == "b") return VirtualKeyCode.VK_B;
            if (c == "c") return VirtualKeyCode.VK_C;
            if (c == "d") return VirtualKeyCode.VK_D;
            if (c == "e") return VirtualKeyCode.VK_E;
            if (c == "f") return VirtualKeyCode.VK_F;
            if (c == "g") return VirtualKeyCode.VK_G;
            if (c == "h") return VirtualKeyCode.VK_H;
            if (c == "i") return VirtualKeyCode.VK_I;
            if (c == "j") return VirtualKeyCode.VK_J;
            if (c == "k") return VirtualKeyCode.VK_K;
            if (c == "l") return VirtualKeyCode.VK_L;
            if (c == "m") return VirtualKeyCode.VK_M;
            if (c == "n") return VirtualKeyCode.VK_N;
            if (c == "o") return VirtualKeyCode.VK_O;
            if (c == "p") return VirtualKeyCode.VK_P;
            if (c == "q") return VirtualKeyCode.VK_Q;
            if (c == "r") return VirtualKeyCode.VK_R;
            if (c == "s") return VirtualKeyCode.VK_S;
            if (c == "t") return VirtualKeyCode.VK_T;
            if (c == "u") return VirtualKeyCode.VK_U;
            if (c == "v") return VirtualKeyCode.VK_V;
            if (c == "w") return VirtualKeyCode.VK_W;
            if (c == "x") return VirtualKeyCode.VK_X;
            if (c == "y") return VirtualKeyCode.VK_Y;
            if (c == "z") return VirtualKeyCode.VK_Z;
            if (c == "0") return VirtualKeyCode.VK_0;
            if (c == "1") return VirtualKeyCode.VK_1;
            if (c == "2") return VirtualKeyCode.VK_2;
            if (c == "3") return VirtualKeyCode.VK_3;
            if (c == "4") return VirtualKeyCode.VK_4;
            if (c == "5") return VirtualKeyCode.VK_5;
            if (c == "6") return VirtualKeyCode.VK_6;
            if (c == "7") return VirtualKeyCode.VK_7;
            if (c == "8") return VirtualKeyCode.VK_8;
            if (c == "9") return VirtualKeyCode.VK_9;
            if (c == ".") return VirtualKeyCode.OEM_PERIOD;
            if (c == "+") return VirtualKeyCode.OEM_PLUS;
            if (c == "-") return VirtualKeyCode.OEM_MINUS;
            if (c == "*") return VirtualKeyCode.MULTIPLY;
            if (c == "/") return VirtualKeyCode.DIVIDE;
            if (c == "del") return VirtualKeyCode.DELETE;
            if (c == "f1") return VirtualKeyCode.F1;
            if (c == "f2") return VirtualKeyCode.F2;
            if (c == "f3") return VirtualKeyCode.F3;
            if (c == "f4") return VirtualKeyCode.F4;
            if (c == "f5") return VirtualKeyCode.F5;
            if (c == "f6") return VirtualKeyCode.F6;
            if (c == "f7") return VirtualKeyCode.F7;
            if (c == "f8") return VirtualKeyCode.F8;
            if (c == "f9") return VirtualKeyCode.F9;
            if (c == "f10") return VirtualKeyCode.F10;
            if (c == "f11") return VirtualKeyCode.F11;
            if (c == "f12") return VirtualKeyCode.F12;
            if (c == "f13") return VirtualKeyCode.F13;
            if (c == "f14") return VirtualKeyCode.F14;
            if (c == "f15") return VirtualKeyCode.F15;
            if (c == "f16") return VirtualKeyCode.F16;
            if (c == "f17") return VirtualKeyCode.F17;
            if (c == "f18") return VirtualKeyCode.F18;
            if (c == "f19") return VirtualKeyCode.F19;
            if (c == "f20") return VirtualKeyCode.F20;
            if (c == "f21") return VirtualKeyCode.F21;
            if (c == "f22") return VirtualKeyCode.F22;
            if (c == "f23") return VirtualKeyCode.F23;
            if (c == "f24") return VirtualKeyCode.F24;
            return 0;
        }

        private static Random random = new Random();
        public static int mouseSpeed = 15;

        public static void MouseMove()
        {
            MoveMouse(0, 0, ScreenWidth(), ScreenHeight());
        }
        public static void MoveMouse()
        {
            MoveMouse(0, 0, ScreenWidth(), ScreenHeight());
        }
        public static void MouseMove(int x, int y)
        {
            MoveMouse(x, y, x, y);
        }
        public static void MoveMouse(int x, int y)
        {
            MoveMouse(x, y, x, y);
        }
        public static void MouseMove(int x1, int y1, int x2, int y2)
        {
            MoveMouse(x1, y1, x2, y2);
        }
        public static void MoveMouse(int x1, int y1, int x2, int y2)
        {
                Point c = new Point();
            GetCursorPos(out c);
            int x = Random(x1, x2), y = Random(y1, y2);
            double randomSpeed = Math.Max((random.Next(mouseSpeed) / 2.0 + mouseSpeed) / 10.0, 0.1);

            WindMouse(c.X, c.Y, x, y, 9.0, 3.0, 10.0 / randomSpeed,
                15.0 / randomSpeed, 10.0 * randomSpeed, 10.0 * randomSpeed);
        }

        private static void WindMouse(double xs, double ys, double xe, double ye,
            double gravity, double wind, double minWait, double maxWait,
            double maxStep, double targetArea)
        {
            PerlinNoise perlinNoise = new PerlinNoise(Random(1,1000000000));
            double dist, windX = 0, windY = 0, veloX = 0, veloY = 0, randomDist, veloMag, step;
            int oldX, oldY, newX = (int)Math.Round(xs), newY = (int)Math.Round(ys);
            dist = Hypot(xe - xs, ye - ys);
            bool fast1 = false, fast2 = false, fast3 = false;
            double waitDiff = maxWait - minWait;
            double sqrt2 = Math.Sqrt(2.0);
            double sqrt3 = Math.Sqrt(3.0);
            double sqrt5 = Math.Sqrt(5.0);

            int count = 0;
            while (dist > 1.0)
            {
                count++;
                wind = Math.Min(wind, dist);
                if (dist >= targetArea)
                {
                    int w = random.Next((int)Math.Round(wind) * 2 + 1);
                    windX = windX / sqrt3 + (w - wind) / sqrt5;
                    windY = windY / sqrt3 + (w - wind) / sqrt5;
                }
                else
                {
                    windX = windX / sqrt2;
                    windY = windY / sqrt2;
                    if (maxStep < 3)
                        maxStep = random.Next(3) + 3.0;
                    else
                        maxStep = maxStep / sqrt5;
                }
                veloX += windX;
                veloY += windY;
                veloX = veloX + gravity * (xe - xs) / dist;
                veloY = veloY + gravity * (ye - ys) / dist;
                if (Hypot(veloX, veloY) > maxStep)
                {
                    randomDist = maxStep / 2.0 + random.Next((int)Math.Round(maxStep) / 2);
                    veloMag = Hypot(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }
                oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                /*if (count > startRND && count < stopRND)
                {
                    veloX += 10 * perlinNoise.Noise(count / 10.0f, 10, 10);
                    veloY += 10 * perlinNoise.Noise(count / 10.0f, 100, 100);
                    veloX += 20 * perlinNoise.Noise(count / 60.0f, 280, 210);
                    veloY += 20 * perlinNoise.Noise(count / 60.0f, 280, 2100);
                }
                if (count > fastStart && count < fastStop)
                {
                    veloX *= 1 + perlinNoise.Noise(count / 10.0f, 80, 10) * 2;
                    veloY *= 1 + perlinNoise.Noise(count / 10.0f, 80, 100) * 2;
                }*/
                if (Random(150) == 0) fast1 = !fast1;
                if (Random(90) == 10) fast2 = !fast2;
                if (Random(70) == 30) fast3 = !fast3;
                if (fast1)
                {
                    veloX += 10 * perlinNoise.Noise(count / 40.0f, 2334, 3456);
                    veloY += 10 * perlinNoise.Noise(count / 40.0f, 1273, 7453);
                }
                if (fast2)
                {
                    veloX += 13 * perlinNoise.Noise(count / 60.0f, 8953, 2345);
                    veloY += 13 * perlinNoise.Noise(count / 60.0f, 5670, 7452);
                }
                if (fast3)
                {
                    veloX += 3 * perlinNoise.Noise(count / 20.0f, 6465, 2346);
                    veloY += 3 * perlinNoise.Noise(count / 20.0f, 3745, 8678);
                }


                if (Random(80) == 0)
                {
                    Sleep(200, 1000);
                    veloX += 10 * perlinNoise.Noise(count / dist, 2343, 5987);
                    veloY += 10 * perlinNoise.Noise(count / dist, 7656, 4735);
                }
                xs += veloX;
                ys += veloY;
                dist = Hypot(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);
                if (newX < 0) newX = 0;
                if (newY < 0) newY = 0;
                if (newX > Screen.PrimaryScreen.WorkingArea.Width) newX = Screen.PrimaryScreen.WorkingArea.Width;
                if (newY > Screen.PrimaryScreen.WorkingArea.Height) newY = Screen.PrimaryScreen.WorkingArea.Height;
                if (oldX != newX || oldY != newY) SetCursorPos(newX, newY);
                step = Hypot(xs - oldX, ys - oldY);
                int wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);
                Thread.Sleep(wait);
            }

            int endX = (int)Math.Round(xe);
            int endY = (int)Math.Round(ye);
            if (endX != newX || endY != newY)
                SetCursorPos(endX, endY);
        }

        private static double Hypot(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static void SetCursor(int x, int y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > Screen.PrimaryScreen.WorkingArea.Width) x = Screen.PrimaryScreen.WorkingArea.Width;
            if (y > Screen.PrimaryScreen.WorkingArea.Height) y = Screen.PrimaryScreen.WorkingArea.Height;
            SetCursorPos(x, y);
            //Cursor.Position = new Point(x, y);
        }

        public static int GetCursorX()
        {
            return Cursor.Position.X;
        }

        public static int GetCursorY()
        {
            return Cursor.Position.Y;
        }

        public static bool IsMouseDown()
        {
            return InputSimulator.IsKeyDown(VirtualKeyCode.LBUTTON);
        }

        public static void Click()
        {
            if (IsMouseDown()) MouseUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_LEFTDOWN, X, Y, 0, 0);
            Sleep(60, 500);
            mouse_event(MOUSE_LEFTUP, X, Y, 0, 0);
        }

        public static void MouseDown()
        {
            if (IsMouseDown()) MouseUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_LEFTDOWN, X, Y, 0, 0);
            Sleep(60, 300);
        }
        public static void MouseUp()
        {
            if (!IsMouseDown()) return;
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_LEFTUP, X, Y, 0, 0);
            Sleep(60, 300);
        }

        public static bool IsRMouseDown()
        {
            return InputSimulator.IsKeyDown(VirtualKeyCode.RBUTTON);
        }

        public static void RightClick()
        {
            if (IsRMouseDown()) RMouseUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_RIGHTDOWN, X, Y, 0, 0);
            Sleep(60, 500);
            mouse_event(MOUSE_RIGHTUP, X, Y, 0, 0);
        }

        public static void RMouseDown()
        {
            if (IsRMouseDown()) RMouseUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_RIGHTDOWN, X, Y, 0, 0);
            Sleep(60, 300);
        }

        public static void RMouseUp()
        {
            if (!IsRMouseDown()) return;
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_RIGHTUP, X, Y, 0, 0);
            Sleep(60, 300);
        }

        public static bool IsMiddleDown()
        {
            return InputSimulator.IsKeyDown(VirtualKeyCode.MBUTTON);
        }

        public static void MiddleClick()
        {
            if (IsMiddleDown()) MiddleUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_MIDDLEDOWN, X, Y, 0, 0);
            Sleep(60, 500);
            mouse_event(MOUSE_MIDDLEUP, X, Y, 0, 0);
        }

        public static void MiddleDown()
        {
            if (IsMiddleDown()) MiddleUp();
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_MIDDLEDOWN, X, Y, 0, 0);
            Sleep(60, 300);
        }

        public static void MiddleUp()
        {
            if (!IsMiddleDown()) return;
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSE_MIDDLEUP, X, Y, 0, 0);
            Sleep(60, 300);
        }

        public static int ScreenWidth()
        {
            return Screen.PrimaryScreen.WorkingArea.Width;
        }

        public static int ScreenHeight()
        {
            return Screen.PrimaryScreen.WorkingArea.Height;
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        
        public static Color GetColorAt(int x, int y)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, x, y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }

        public static void Break(string msg)
        {
            Application.Restart();
            Alert(msg);
        }

        public static void Break()
        {
            Application.Restart();
        }

        public static void _eof() { }
    }

    public class PerlinNoise
    {
        private const int GradientSizeTable = 256;
        private readonly Random _random;
        private readonly double[] _gradients = new double[GradientSizeTable * 3];
        private readonly byte[] _perm = new byte[] {
              225,155,210,108,175,199,221,144,203,116, 70,213, 69,158, 33,252,
                5, 82,173,133,222,139,174, 27,  9, 71, 90,246, 75,130, 91,191,
              169,138,  2,151,194,235, 81,  7, 25,113,228,159,205,253,134,142,
              248, 65,224,217, 22,121,229, 63, 89,103, 96,104,156, 17,201,129,
               36,  8,165,110,237,117,231, 56,132,211,152, 20,181,111,239,218,
              170,163, 51,172,157, 47, 80,212,176,250, 87, 49, 99,242,136,189,
              162,115, 44, 43,124, 94,150, 16,141,247, 32, 10,198,223,255, 72,
               53,131, 84, 57,220,197, 58, 50,208, 11,241, 28,  3,192, 62,202,
               18,215,153, 24, 76, 41, 15,179, 39, 46, 55,  6,128,167, 23,188,
              106, 34,187,140,164, 73,112,182,244,195,227, 13, 35, 77,196,185,
               26,200,226,119, 31,123,168,125,249, 68,183,230,177,135,160,180,
               12,  1,243,148,102,166, 38,238,251, 37,240,126, 64, 74,161, 40,
              184,149,171,178,101, 66, 29, 59,146, 61,254,107, 42, 86,154,  4,
              236,232,120, 21,233,209, 45, 98,193,114, 78, 19,206, 14,118,127,
               48, 79,147, 85, 30,207,219, 54, 88,234,190,122, 95, 67,143,109,
              137,214,145, 93, 92,100,245,  0,216,186, 60, 83,105, 97,204, 52};
        public PerlinNoise(int seed)
        {
            _random = new Random(seed);
            InitGradients();
        }

        public double Noise(double x, double y, double z)
        {
            int ix = (int)Math.Floor(x);
            double fx0 = x - ix;
            double fx1 = fx0 - 1;
            double wx = Smooth(fx0);
            int iy = (int)Math.Floor(y);
            double fy0 = y - iy;
            double fy1 = fy0 - 1;
            double wy = Smooth(fy0);
            int iz = (int)Math.Floor(z);
            double fz0 = z - iz;
            double fz1 = fz0 - 1;
            double wz = Smooth(fz0);
            double vx0 = Lattice(ix, iy, iz, fx0, fy0, fz0);
            double vx1 = Lattice(ix + 1, iy, iz, fx1, fy0, fz0);
            double vy0 = Lerp(wx, vx0, vx1);
            vx0 = Lattice(ix, iy + 1, iz, fx0, fy1, fz0);
            vx1 = Lattice(ix + 1, iy + 1, iz, fx1, fy1, fz0);
            double vy1 = Lerp(wx, vx0, vx1);
            double vz0 = Lerp(wy, vy0, vy1);
            vx0 = Lattice(ix, iy, iz + 1, fx0, fy0, fz1);
            vx1 = Lattice(ix + 1, iy, iz + 1, fx1, fy0, fz1);
            vy0 = Lerp(wx, vx0, vx1);
            vx0 = Lattice(ix, iy + 1, iz + 1, fx0, fy1, fz1);
            vx1 = Lattice(ix + 1, iy + 1, iz + 1, fx1, fy1, fz1);
            vy1 = Lerp(wx, vx0, vx1);
            double vz1 = Lerp(wy, vy0, vy1);
            return Lerp(wz, vz0, vz1);
        }

        private void InitGradients()
        {
            for (int i = 0; i < GradientSizeTable; i++)
            {
                double z = 1f - 2f * _random.NextDouble();
                double r = Math.Sqrt(1f - z * z);
                double theta = 2 * Math.PI * _random.NextDouble();
                _gradients[i * 3] = r * Math.Cos(theta);
                _gradients[i * 3 + 1] = r * Math.Sin(theta);
                _gradients[i * 3 + 2] = z;
            }
        }

        private int Permutate(int x)
        {
            const int mask = GradientSizeTable - 1;
            return _perm[x & mask];
        }

        private int Index(int ix, int iy, int iz)
        {
            // Turn an XYZ triplet into a single gradient table index.
            return Permutate(ix + Permutate(iy + Permutate(iz)));
        }

        private double Lattice(int ix, int iy, int iz, double fx, double fy, double fz)
        {
            // Look up a random gradient at [ix,iy,iz] and dot it with the [fx,fy,fz] vector.
            int index = Index(ix, iy, iz);
            int g = index * 3;
            return _gradients[g] * fx + _gradients[g + 1] * fy + _gradients[g + 2] * fz;
        }

        private double Lerp(double t, double value0, double value1)
        {
            // Simple linear interpolation.
            return value0 + t * (value1 - value0);
        }

        private double Smooth(double x)
        {
            /* Smoothing curve. This is used to calculate interpolants so that the noise
              doesn't look blocky when the frequency is low. */
            return x * x * (3 - 2 * x);
        }
    }
}