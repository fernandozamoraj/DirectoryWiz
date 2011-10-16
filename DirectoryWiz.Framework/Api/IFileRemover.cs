using DirectoryWiz.Framework.MatchEvaluators;

namespace DirectoryWiz.Framework.Api
{
    public interface IFileRemover
    {
        void RemoveFileByExtensions(string root, string[] extensions);
        void RemoveFileByName(string root, string[] names);
        void RemoveFolderByName(string root, string[] names);
        void RemoveFilesByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator);
        void RemoveFoldersByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator);
    }
}