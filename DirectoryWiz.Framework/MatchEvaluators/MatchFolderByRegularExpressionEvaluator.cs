using System.Text.RegularExpressions;

namespace DirectoryWiz.Framework.MatchEvaluators
{
    public class MatchFolderByRegularExpressionEvaluator : IMatchFileByEvaluator
    {
        private string _pattern;

        public MatchFolderByRegularExpressionEvaluator(string pattern)
        {
            _pattern = pattern;
        }

        public bool IsMatch(string folder)
        {
            Regex regex = new Regex(_pattern);

            return regex.IsMatch(folder); 
        }
    }
}