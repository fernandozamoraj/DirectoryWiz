using System;
using DirectoryWiz.Framework;
using DirectoryWiz.Framework.Api;

namespace DirWizConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralFileRemover fileRemover =

                new GeneralFileRemover();

            fileRemover.ErrorOccurred += new System.EventHandler<FileActionEventArgs>(fileRemover_ErrorOccurred);
            fileRemover.OnProgress += new System.EventHandler<FileActionEventArgs>(fileRemover_OnProgress);

            fileRemover.RemoveFolderByName(@"C:\temp\New folder", new string[] { ".svn", "obj", "bin", "debug" });

            Console.ReadLine();
        }

        static void fileRemover_OnProgress(object sender, FileActionEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        static void fileRemover_ErrorOccurred(object sender, FileActionEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        static void Sample()
        {

            IFileCopier fileCopier = new GeneralFileCopier();

            fileCopier.CopyFull("C:\\Temp\\ahmed", "C:\\Temp\\abcd\\ahmed");
            fileCopier.CopyFullToFlat("C:\\Temp\\ahmed", "C:\\Temp\\abcd\\ahmedflat");
            fileCopier.CopyFullToFlat("C:\\Temp\\ahmed", "C:\\Temp\\abcd\\imagesflat", new string[] { ".bmp", ".png" });


            IFileRemover fileRemover =
                new GeneralFileRemover();

            fileRemover.RemoveFolderByName("G:\\Programming Apps All", new string[] { ".svn", "obj", "bin" });

        }
    }
}
