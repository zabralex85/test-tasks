using System;
using System.Collections.Generic;

namespace Alemira.RandomData.Lib.Collections
{
    public class CaseSensitiveComparator : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.InvariantCulture);
        }
    }
}