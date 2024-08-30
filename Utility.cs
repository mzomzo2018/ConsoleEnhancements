using MuhammadAl_ZubairObaid.Utilities.DataModels;
using System.Drawing;
using System.Text;

namespace MuhammadAl_ZubairObaid.Utilities
{
    /// <summary>
    /// Provides extended methods to write data to <see cref="Console"/>
    /// </summary>
    public static partial class ExtendedConsole
    {
        /// <summary>
        /// Writes required text to <see cref="Console"/>
        /// <para>You can control text colorization fluently.</para>
        /// </summary>
        /// <param name="value">Required text</param>
        /// <remarks><para>You must specify the first color to display your text. Otherwise, <see cref="ArgumentOutOfRangeException"/> will be thrown. <br></br>When specifing more than one color attributes. [S] must be present between every colorized sentence.</para></remarks>
        public static void Write(string value)
        {
            // Setting Console.InputEncoding and Console.OutputEncoding to Encoding.Unicode is benefitial for Unicode languages, like Arabic 
            // See README.md regarding Right-To-Left text output.
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            // Splitting input text based on [S] attributes
            string[] splitValues = value.Split("[S]");
            foreach (string splitValue in splitValues)
            {
                // Converting color attribute into ConsoleColor value
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), splitValue.AsSpan(splitValue.IndexOf('[') + 1, splitValue.IndexOf(']') - 1));
                // Removing color attribute, which is neglicted
                Console.Write(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                // Resetting color to apply other color attributes
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Writes required text to <see cref="Console"/>
        /// <para>You can control text colorization fluently.</para>
        /// </summary>
        /// <param name="value">Required text</param>
        /// <remarks><para>You must specify the first color to display your text. Otherwise, <see cref="ArgumentOutOfRangeException"/> will be thrown. <br></br>When specifing more than one color attributes. [S] must be present between every colorized sentence.</para></remarks>
        public static void WriteLine(string value)
        {
            // Setting Console.InputEncoding and Console.OutputEncoding to Encoding.Unicode is benefitial for Unicode languages, like Arabic 
            // See README.md regarding Right-To-Left text output.
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            // Splitting input text based on [S] attributes
            string[] splitValues = value.Split("[S]");
            foreach (string splitValue in splitValues)
            {
                // Converting color attribute into ConsoleColor value
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), splitValue.AsSpan(splitValue.IndexOf('[') + 1, splitValue.IndexOf(']') - 1));
                // Colorized sentences must not be written in new lines
                if (splitValue != splitValues.Last())
                    // Removing color attribute, which is neglicted
                    Console.Write(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                // Last sentence is exceptional
                else
                    Console.WriteLine(splitValue.Replace(splitValue.Substring(splitValue.IndexOf('['), splitValue.IndexOf(']') + 1), ""));
                // Resetting color to apply other color attributes
                Console.ResetColor();
            }
        }
    }
}
