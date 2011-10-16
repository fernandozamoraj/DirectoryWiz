using System.IO;

namespace DirectoryWiz.Framework.MatchEvaluators
{
    public class MatchFolderByNameEvaluator : IMatchFileByEvaluator
    {
        private string[] _folderNames;

        public MatchFolderByNameEvaluator(string[] folderNames)
        {
            _folderNames = folderNames;
        }

        public bool IsMatch(string folder)
        {
            foreach (string folderName in _folderNames)
            {
                if (!string.IsNullOrEmpty(folderName) &&
                    (
                        folder.ToUpper().Contains(("\\" + folderName  + "\\").ToUpper()) ||
                        folder.ToUpper().EndsWith(("\\" + folderName).ToUpper())))
                    return true;
            }

            return false;
        }
    }
}