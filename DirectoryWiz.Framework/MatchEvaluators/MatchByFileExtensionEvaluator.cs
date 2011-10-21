namespace DirectoryWiz.Framework.MatchEvaluators
{
    public class MatchByFileExtensionEvaluator : IMatchFileByEvaluator
    {
        private string[] _fileExtensions;

        public MatchByFileExtensionEvaluator(string[] fileExtensios)
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