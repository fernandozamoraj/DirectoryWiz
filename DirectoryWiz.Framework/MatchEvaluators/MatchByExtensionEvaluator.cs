namespace DirectoryWiz.Framework.MatchEvaluators
{
    public class MatchByExtensionEvaluator : IMatchFileByEvaluator
    {
        private string[] _fileExtensions;

        public MatchByExtensionEvaluator(string[] fileExtensios)
        {
            _fileExtensions = fileExtensios;
        }

        public bool IsMatch(string file)
        {
            foreach(string fileExtension in _fileExtensions)
            {
                if(file.ToUpper().EndsWith(fileExtension.ToUpper()))
                    return true;
            }

            return false;
        }
    }
}