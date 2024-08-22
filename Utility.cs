using System.Drawing;
using System.Text;

namespace MuhammadAl_ZubairObaid.Utilities
{
    public static class ExtendedConsole
    {
        /// <summary>
        /// Displays Console Form Language compilation to console.
        /// <para>This method works effectively in <c>CONHOST.EXE</c></para>
        /// </summary>
        /// <param name="cflPath">Console Form Language file to compile and display</param>
        /// <returns>Pressed key</returns>
        static void DisplayCFL(ref string cflPath, out ConsoleKey key)
        {
            Console.Clear();
            var form = CompileCFL(cflPath);
            Console.SetWindowSize(form.Width, form.Height);
            WriteLine("[White]" + form.FormString);
            if (form.CursorPositions!.Count() == 0)
                Console.SetCursorPosition(0, 0);
            else
                Console.SetCursorPosition(form.CursorPositions![0].X, form.CursorPositions![0].Y);
            key = Console.ReadKey().Key;
            string nextFile = "";
            if (key == ConsoleKey.DownArrow)
            {
                nextFile = cflPath.Replace(Path.GetFileNameWithoutExtension(cflPath).Last().ToString(), (int.Parse(Path.GetFileNameWithoutExtension(cflPath).Last().ToString()) + 1).ToString());
                if (File.Exists(nextFile))
                    DisplayCFL(ref nextFile, out key);
                else
                    DisplayCFL(ref cflPath, out key);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                nextFile = cflPath.Replace(Path.GetFileNameWithoutExtension(cflPath).Last().ToString(), (int.Parse(Path.GetFileNameWithoutExtension(cflPath).Last().ToString()) - 1).ToString());
                if (File.Exists(nextFile))
                    DisplayCFL(ref nextFile, out key);
                else
                    DisplayCFL(ref cflPath, out key);
            }
            else if (key == ConsoleKey.Enter)
            { Console.Clear(); return; }
            else
                DisplayCFL(ref cflPath, out key);
        }
        static void DisplayCFL(ref string cflPath)
        {
            Console.Clear();
            var form = CompileCFL(cflPath);
            Console.SetWindowSize(form.Width, form.Height);
            WriteLine("[White]" + form.FormString);
            Console.SetCursorPosition(0, 0);
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cflPath"></param>
        /// <param name="line"></param>
        static void DisplayCFL(ref string cflPath, out string line, params string[] attributeValues)
        {
            Console.Clear();
            List<CFLAttribute> cFLAttributes = new List<CFLAttribute>();
            foreach (var attributeValue in attributeValues)
                cFLAttributes.Add(new CFLAttribute() { IsApplied = false, Value = attributeValue });
            var form = CompileCFL(cflPath, [.. cFLAttributes]);
            Console.SetWindowSize(form.Width, form.Height + 3);
            WriteLine("[White]" + form.FormString);
            if (form.CursorPositions!.Count() == 1)
                Console.SetCursorPosition(0, 0);
            else
                Console.SetCursorPosition(form.CursorPositions![1].X, form.CursorPositions![1].Y);
            line = Console.ReadLine()!;
            Console.Clear();
        }

        public static void Write(string value)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            string[] splitValues = value.Split("[S]");
            foreach (string splitValue in splitValues)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), splitValue.Substring(splitValue.IndexOf('[') + 1, splitValue.IndexOf(']') - 1));
                Console.Write(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                Console.ResetColor();
            }
        }
        public static void WriteLine(string value)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            string[] splitValues = value.Split("[S]");
            foreach (string splitValue in splitValues)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), splitValue.Substring(splitValue.IndexOf('[') + 1, splitValue.IndexOf(']') - 1));
                if (splitValue != splitValues.Last())
                    Console.Write(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                else
                    Console.WriteLine(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                Console.ResetColor();
            }
        }
        public static CFLCompilationResult CompileCFL(string cflPath, CFLAttribute[] attributeValues = null)
        {
            var formString = "";
            var statements = File.ReadAllLines(cflPath);
            var width = 0;
            List<Point> cursorPositions = [new Point(0, 0)];
            foreach (var statement in statements)
            {
                var functionName = statement[..statement.IndexOf('(')];
                var functionParameters = statement.Substring(statement.IndexOf('(') + 1, statement.IndexOf(')') - 2).Split(',');
                if (functionName == "=")
                {
                    for (int i = 0; i < int.Parse(functionParameters[0]); i++)
                        formString += '=';
                    width = int.Parse(functionParameters[0]);
                }
                else if (functionName == "|")
                {
                    var line = "";
                    if (functionParameters.Length >= 3)
                    {
                        var stringWithoutAttributes = functionParameters[1];
                        var count = stringWithoutAttributes.Where(c => c == '{').Count();
                        for (int i = 0; i < count; i++)
                        {
                            var attribute = functionParameters[1].Substring(stringWithoutAttributes.IndexOf("{"), stringWithoutAttributes.IndexOf("}") - stringWithoutAttributes.IndexOf("{") + 1);
                            if (attributeValues != null && attributeValues.Length > 0 && !attribute.StartsWith("{["))
                            {
                                foreach (var attributeValue in attributeValues)
                                {
                                    if (!attributeValue.IsApplied)
                                    {
                                        functionParameters[1] = stringWithoutAttributes = functionParameters[1].Replace(attribute, attributeValue.Value);
                                        attributeValue.IsApplied = true;
                                        break;
                                    }
                                }
                            }
                            else
                                stringWithoutAttributes = stringWithoutAttributes.Remove(stringWithoutAttributes.IndexOf("{"), stringWithoutAttributes.IndexOf("}") - stringWithoutAttributes.IndexOf("{") + 1);
                        }
                        functionParameters[1] = functionParameters[1].Replace("{", "");
                        functionParameters[1] = functionParameters[1].Replace("}", "");
                        var centerPosition = (int.Parse(functionParameters[2]) - stringWithoutAttributes.Length) / 2;
                        for (int i = 0; i < int.Parse(functionParameters[2]) + 2; i++)
                        {
                            if (i == int.Parse(functionParameters[0]) - 1)
                                line += '|';
                            else if (i == centerPosition)
                            {
                                var unquotedString = "";
                                unquotedString = functionParameters[1].Substring(functionParameters[1].IndexOf('\"') + 1, functionParameters[1].IndexOf('\"', functionParameters[1].IndexOf('\"') + 1) - 1);
                                if (functionParameters[1].Contains("["))
                                    i += stringWithoutAttributes.Length;
                                else
                                    i += functionParameters[1].Length;
                                line += unquotedString;
                            }
                            else
                                line += ' ';
                        }
                        line += '|';
                        if (functionParameters.Length == 4)
                            cursorPositions.Add(new Point(centerPosition + int.Parse(functionParameters[3]) + 1, statements.ToList().IndexOf(statement)));
                    }
                    else if (functionParameters.Length == 2)
                    {
                        for (int i = 0; i < int.Parse(functionParameters[1]) - 1; i++)
                        {
                            if (i == int.Parse(functionParameters[0]) - 1)
                                line += '|';
                            else
                                line += ' ';
                        }
                        line += '|';
                    }
                    formString += line;
                }
                formString += "\r\n";
            }
            return new CFLCompilationResult(formString, width, statements.Length, [.. cursorPositions]);
        }
    }
    public class CFLCompilationResult
    {
        public string FormString { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public Point[]? CursorPositions { get; init; }
        internal CFLCompilationResult(string formString, int width, int height, Point[]? cursorPositions = null)
        {
            FormString = formString;
            Width = width;
            Height = height;
            CursorPositions = cursorPositions;
        }
    }
    public class CFLAttribute
    {
        public string Value { get; set; }
        public bool IsApplied { get; set; }
    }
}
