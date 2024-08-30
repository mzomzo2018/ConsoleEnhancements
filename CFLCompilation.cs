using MuhammadAl_ZubairObaid.Utilities.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// Compiles Console Form Language file into form-like text with <see cref="Console"/>-controlling options
        /// </summary>
        /// <param name="cflPath">Required CFL file</param>
        /// <param name="attributeValues">CFL attribute values, specified in CFL script</param>
        /// <returns>CFL compilation result</returns>
        public static CFLCompilationResult CompileCFL(string cflPath, CFLAttribute[]? attributeValues = null)
        {
            // Initializing local variables
            var formString = "";
            Point? cursorPosition = null;
            var statements = File.ReadAllLines(cflPath);
            var width = 0;
            // Parsing statements
            foreach (var statement in statements)
            {
                // Substrings statements from index 0 to ( index to get function name
                var functionName = statement[..statement.IndexOf('(')];
                // Function parameters starts from ( (Not counted) and ends with ) (Not counted), split by semicolons.
                var functionParameters = statement.Substring(statement.IndexOf('(') + 1, statement.IndexOf(')') - 2).Split(',');
                // Currently supported functions are | and =.
                // Defaultly used as top and bottom borders. You can also add more equal sign bars.
                if (statement == statements.First() || statement == statements.Last() || functionName == "=")
                {
                    // Function expression: =(int count)
                    for (int i = 0; i < int.Parse(functionParameters[0]); i++)
                        // Adding equal signs (count) times.
                        formString += '=';
                    // Setting console window with to (count) value.
                    // The reason behind the assignment because equal sign bars generally used as top and bottom borders.
                    width = int.Parse(functionParameters[0]);
                }
                else if (functionName == "|")
                {
                    // Initializing local variables 
                    var line = "";
                    // Function expression: |(int startIndex, string centerizedText, int endIndex)
                    if (functionParameters.Length >= 3)
                    {
                        // stringWithoutAttributes default value is same (centerizedText), which generally does not contain color attributes
                        var stringWithoutAttributes = functionParameters[1];
                        // Removing attributes from (centerizedText)
                        // Counting variables and attributes
                        var count = stringWithoutAttributes.Where(c => c == '{').Count();
                        // Replacing variables and attributes with defined values
                        for (int i = 0; i < count; i++)
                        {
                            // Attribute names start from { index (Not counted) and ends with } index (Not counted), therefore attribute length is } index subtracted { index
                            var attribute = functionParameters[1].Substring(stringWithoutAttributes.IndexOf('{'), stringWithoutAttributes.IndexOf('}') - stringWithoutAttributes.IndexOf('{') + 1);
                            // Applying attribute values, color attributes are currently excluded 
                            // attributeValues.Length > 0 is necessary to navigate to ELSE block.
                            if (attributeValues != null && attributeValues.Length > 0 && !attribute.StartsWith("{["))
                            {
                                foreach (var attributeValue in attributeValues)
                                {
                                    // Attribute value replacement must not be already applied.
                                    if (!attributeValue.IsApplied)
                                    {
                                        // Replacing attribute name with its value
                                        functionParameters[1] = functionParameters[1].Replace(attribute, attributeValue.Value);
                                        attributeValue.IsApplied = true;
                                        break;
                                    }
                                }
                            }
                            // Removing { and } from stringWithoutAttributes
                            else
                                stringWithoutAttributes = stringWithoutAttributes.Remove(stringWithoutAttributes.IndexOf("{"), stringWithoutAttributes.IndexOf("}") - stringWithoutAttributes.IndexOf("{") + 1);
                        }
                        // Removing { and } from (centerizedText}
                        functionParameters[1] = functionParameters[1].Replace("{", "");
                        functionParameters[1] = functionParameters[1].Replace("}", "");
                        // Centered position is (endIndex) subtracted by (centerizedText) without attributes, then divided by 2
                        var centerPosition = (int.Parse(functionParameters[2]) - stringWithoutAttributes.Length) / 2;
                        // (endIndex) is increased by 2 for console cursor position adjustments.
                        for (int i = 0; i < int.Parse(functionParameters[2]) + 2; i++)
                        {
                            // Starting current line with |
                            if (i == int.Parse(functionParameters[0]) - 1)
                                line += '|';
                            // Writing centerized text at center postition
                            else if (i == centerPosition)
                            {
                                // Removing quotions
                                var unquotedString = "";
                                unquotedString = functionParameters[1].Substring(functionParameters[1].IndexOf('\"') + 1, functionParameters[1].IndexOf('\"', functionParameters[1].IndexOf('\"') + 1) - 1);
                                // If centerizedText contains attributes, then line position is increased by stringWithoutAttributes length
                                if (functionParameters[1].Contains("["))
                                    i += stringWithoutAttributes.Length;
                                // Otherwise, the line position is increased by (centerizedText) length
                                else
                                    i += functionParameters[1].Length;
                                // Appending unquoted text to current line
                                line += unquotedString;
                            }
                            else
                                // Appending spaces to current text
                                line += ' ';
                        }
                        // Ending current line with |
                        line += '|';
                        // Function expression: |(int startIndex, string centerizedText, int endIndex, int cursorPosition)
                        // Adding specified cursor position.
                        if (functionParameters.Length == 4)
                            // Generally, (cursorPosition) should be inside (centerizedText). Y-axis value is line index
                            cursorPosition = new Point(centerPosition + int.Parse(functionParameters[3]) + 1, statements.ToList().IndexOf(statement));

                    }
                    // Function expression: |(int startIndex, int endIndex)
                    else if (functionParameters.Length == 2)
                    {
                        // Adding empty line starting and ending with |, split by spaces.
                        for (int i = 0; i < int.Parse(functionParameters[1]) - 1; i++)
                        {
                            if (i == int.Parse(functionParameters[0]) - 1)
                                line += '|';
                            else
                                line += ' ';
                        }
                        line += '|';
                    }
                    // Adding current line to general form string.
                    formString += line;
                }
                // Breaking new line
                formString += "\r\n";
            }
            // Instanciating CFLCompilationResult with parameters assigned to local variables
            return new CFLCompilationResult(formString, width, statements.Length, cursorPosition);
        }
    }
}
