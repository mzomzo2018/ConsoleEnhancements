using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhammadAl_ZubairObaid.Utilities.DataModels
{
    /// <summary>
    /// Represents Console Form Language compilation result
    /// </summary>
    public class CFLCompilationResult
    {
        /// <summary>
        /// General form string, used to be printed to Console
        /// </summary>
        public string FormString { get; init; }
        /// <summary>
        /// Form width, used to set console window width
        /// </summary>
        public int Width { get; init; }
        /// <summary>
        /// Form height, used to set console window height
        /// </summary>
        public int Height { get; init; }
        /// <summary>
        /// Console cursor position
        /// </summary>
        public Point? CursorPosition { get; init; }
        /// <summary>
        /// Initializes new instance of <see cref="CFLCompilationResult"/>
        /// </summary>
        /// <param name="formString">General form string</param>
        /// <param name="width">Form width</param>
        /// <param name="height">Form height</param>
        /// <param name="cursorPosition">Console cursor position</param>
        internal CFLCompilationResult(string formString, int width, int height, Point? cursorPosition = null)
        {
            FormString = formString;
            Width = width;
            Height = height;
            CursorPosition = cursorPosition;
        }
    }
    /// <summary>
    /// Used to represent attributes defined in string parameters in CFL functions
    /// </summary>
    public class CFLAttribute
    {
        /// <summary>
        /// Attribute values
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Attribute application
        /// </summary>
        public bool IsApplied { get; set; }
    }

}
