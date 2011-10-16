namespace DirectoryWiz.Framework.MatchEvaluators
{
    public class MatchByNameEvaluator : IMatchFileByEvaluator
    {
        private string[] _fileNames;

        public MatchByNameEvaluator(string[] fileNames)
        {
            _fileNames = fileNames;
        }

        public bool IsMatch(string file)
        {
            foreach (string fileName in _fileNames)
            {
                if (file.ToUpper().EndsWith(fileName.ToUpper()))
                    return true;
            }

            return false;
        }
    }
}