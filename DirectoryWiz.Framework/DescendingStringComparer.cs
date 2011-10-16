using System.Collections.Generic;

namespace DirectoryWiz.Framework
{
    public class DescendingStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return y.CompareTo(x);
        }
    }
}