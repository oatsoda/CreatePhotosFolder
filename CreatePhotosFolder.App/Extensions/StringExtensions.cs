using System;

namespace CreatePhotosFolder.App.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Shortcut for string.Compare using StringComparison.InvariantCultureIgnoreCase.
        /// </summary>
        public static bool IsSameStringValue(this string input, string compare) => 
            string.Compare(input, compare, StringComparison.InvariantCultureIgnoreCase) == 0;
    }
}
