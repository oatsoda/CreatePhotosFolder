using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePhotosFolder.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Shortcut for string.Compare using StringComparison.InvariantCultureIgnoreCase.
        /// </summary>
        public static bool IsSame(this string input, string compare) => 
            string.Compare(input, compare, StringComparison.InvariantCultureIgnoreCase) == 0;
    }
}
