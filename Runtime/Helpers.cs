using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace com.absence.utilities
{
    /// <summary>
    /// Holds some handy functions.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Splits input string by capital letters and returns all seperated parts.
        /// </summary>
        public static IEnumerable<string> SplitCamelCase(string input)
        {
            return Regex.Split(input, @"([A-Z]?[a-z]+)").Where(str => !string.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Splits input string by capital letters and returns a more human readable one.
        /// </summary>
        public static string SplitCamelCaseWithSpace(string input, string seperator)
        {
            return String.Join(seperator, SplitCamelCase(input));
        }
    }

}
