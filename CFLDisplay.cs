using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhammadAl_ZubairObaid.Utilities
{
    /// <summary>
    /// Provides extended methods to write data to <see cref="Console"/>
    /// </summary>
    public static partial class ExtendedConsole
    {
        /// <summary>
        /// Displays Console Form Language compilation to console as selection form.
        /// <para>This method works effectively in <c>CONHOST.EXE</c></para>
        /// </summary>
        /// <param name="cflPath">Referenced Console Form Language file to compile and display</param>
        /// <param name="key">Pressed console key as output</param>
        /// <param name="attributeValues">Attributes specified in CFL function parameters</param>
        public static void DisplayCFL(ref string cflPath, out ConsoleKey key, params string[] attributeValues)
        {
            // Clearing console every display process
            Console.Clear();
            // Converting string array into CFLAttribute list with instanciated CFLAttribute objects.
            List<CFLAttribute> cFLAttributes = new List<CFLAttribute>();
            foreach (var attributeValue in attributeValues)
                cFLAttributes.Add(new CFLAttribute() { IsApplied = false, Value = attributeValue });
            // Compiling specified CFL file
            var form = CompileCFL(cflPath, [.. cFLAttributes]);
            // Appyling CFLCompilationResult properties
            Console.SetWindowSize(form.Width, form.Height);
            // Default color used for ExtendedConsole.WriteLine(), which first color attribute used to display is required
            WriteLine("[White]" + form.FormString);
            // Setting console cursor position to first specified cursor position
            if (form.CursorPosition is not null)
                Console.SetCursorPosition(0, 0);
            else
                Console.SetCursorPosition(form.CursorPosition!.Value.X, form.CursorPosition!.Value.Y);
            // Reading ConsoleKey input from user
            key = Console.ReadKey().Key;
            // Initializing next CFL file path
            string nextPreviousFile = "";
            // Controlling current CFL page using keyboard arrow and ENTER keys
            if (key == ConsoleKey.DownArrow)
            {
                // Replacing CFL file index: CFLFile.1.cfl -> CFLFile.2.cfl
                nextPreviousFile = cflPath.Replace(Path.GetFileNameWithoutExtension(cflPath).Last().ToString(), (int.Parse(Path.GetFileNameWithoutExtension(cflPath).Last().ToString()) + 1).ToString());
                // Displaying next CFL file based on file existance recursively.
                // If next file is not found, then the same form appears
                if (File.Exists(nextPreviousFile))
                    DisplayCFL(ref nextPreviousFile, out key);
                else
                    DisplayCFL(ref cflPath, out key);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                // Replacing CFL file index: CFLFile.2.cfl -> CFLFile.1.cfl
                nextPreviousFile = cflPath.Replace(Path.GetFileNameWithoutExtension(cflPath).Last().ToString(), (int.Parse(Path.GetFileNameWithoutExtension(cflPath).Last().ToString()) - 1).ToString());
                // Displaying next CFL file based on file existance recursively.
                // If previous file is not found, then the same form appears
                if (File.Exists(nextPreviousFile))
                    DisplayCFL(ref nextPreviousFile, out key);
                else
                    DisplayCFL(ref cflPath, out key);
            }
            // Clearing console when pressing ENTER key
            else if (key == ConsoleKey.Enter)
            { Console.Clear(); return; }
            // Displaying when other keys are mistakenly pressed
            else
                DisplayCFL(ref cflPath, out key);
        }
        /// <summary>
        /// Displays Console Form Language compilation to console as input form.
        /// <para>This method works effectively in <c>CONHOST.EXE</c></para>
        /// </summary>
        /// <param name="cflPath">Referenced Console Form Language file to compile and display</param>
        /// <param name="attributeValues">Attributes specified in CFL function parameters</param>
        public static void DisplayCFL(ref string cflPath, params string[] attributeValues)
        {
            // Clearing console every display process
            Console.Clear();
            // Converting string array into CFLAttribute list with instanciated CFLAttribute objects.
            List<CFLAttribute> cFLAttributes = new List<CFLAttribute>();
            foreach (var attributeValue in attributeValues)
                cFLAttributes.Add(new CFLAttribute() { IsApplied = false, Value = attributeValue });
            // Compiling specified CFL file
            var form = CompileCFL(cflPath, [.. cFLAttributes]);
            // Appyling CFLCompilationResult properties
            Console.SetWindowSize(form.Width, form.Height);
            // Default color used for ExtendedConsole.WriteLine(), which first color attribute used to display is required
            WriteLine("[White]" + form.FormString);
            // Console cursor position should be partially hidden, which should not be notified by user.
            Console.SetCursorPosition(0, 0);
            return;
        }
        /// <summary>
        /// Displays Console Form Language compilation to console as input form.
        /// <para>This method works effectively in <c>CONHOST.EXE</c></para>
        /// </summary>
        /// <param name="cflPath">Referenced Console Form Language file to compile and display</param>
        /// <param name="line">Read line, which entered by user</param>
        /// <param name="attributeValues">Attributes specified in CFL function parameters</param>
        public static void DisplayCFL(ref string cflPath, out string line, params string[] attributeValues)
        {
            // Clearing console every display process
            Console.Clear();
            // Converting string array into CFLAttribute list with instanciated CFLAttribute objects.
            List<CFLAttribute> cFLAttributes = new List<CFLAttribute>();
            foreach (var attributeValue in attributeValues)
                cFLAttributes.Add(new CFLAttribute() { IsApplied = false, Value = attributeValue });
            // Compiling specified CFL file
            var form = CompileCFL(cflPath, [.. cFLAttributes]);
            // Appyling CFLCompilationResult properties
            Console.SetWindowSize(form.Width, form.Height);
            // Default color used for ExtendedConsole.WriteLine(), which first color attribute used to display is required
            WriteLine("[White]" + form.FormString);
            // Default cursor position
            if (form.CursorPosition is not null)
                Console.SetCursorPosition(0, 0);
            // User-defined cursor position
            else
                Console.SetCursorPosition(form.CursorPosition!.Value.X, form.CursorPosition.Value.Y);
            // Reading user input
            line = Console.ReadLine()!;
            // Clearing console window to display new page
            Console.Clear();
        }
    }
}
