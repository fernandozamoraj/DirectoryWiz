This product is licensed under the MIT Open Source Initiative License agreeement.
Please read the License.txt document for more information.


Usage:

Clean Folders of subfolders and files
Copy directories or files from source location to target location.
Although the recommended usage is through the DirecoryWiz.Framework.Api, I have plans
to provide a command line interface for folder cleanup.

************************************************
*           GeneralFileRemover                 *
************************************************
Clean Folder and all child folders recursively of files.
Files can be targeted by extension, by file name, or by custom evaluator which
can be anything (e.g. Regular Expressions).

.RemoveFileByExtensions(string root, string[] extensions)
To remove all files in the folder and any child folders with the file extensions
    
.RemoveFileByName(string root, string[] names)
To Remove Files by name
    
.RemoveFilesByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator)
To RemoveFies by a custom evaluator
    
.RemoveFoldersByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator)
To RemoveFiles by a custom evaluator
    
To create your own custom evaluator just implement the method
bool IMatchFileEvaluator.IsMatch(string file);
You the file passed into it is the full file name including the directory
    
The following code will remove all subfolders with along with all their files from 
the path C:\path\to\MyProject
The folders .svn obj bin and debug will all be removed when the following code runs.
    
    using System;
    using DirectoryWiz.Framework.Api;

    namespace DirWizConsole
    {
        class Program
        {
            //Example Usage:
            static void Main(string[] args)
            {
                GeneralFileRemover fileRemover = new GeneralFileRemover();

                fileRemover.ErrorOccurred += new System.EventHandler<FileActionEventArgs>(fileRemover_ErrorOccurred);
                fileRemover.OnProgress += new System.EventHandler<FileActionEventArgs>(fileRemover_OnProgress);

                fileRemover.RemoveFolderByName(@"C:\path\to\MyProject", new string[] { ".svn", "obj", "bin", "debug" });

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
       }
    }
    
************************************************
*           GeneralFileCopier                  *
************************************************


    .CopyStructure(string sourceFolder, string targetFolder)
    Copies the structure of the folder from source to target.  It does not
    copy the files.
	
    .CopyFull(string sourceFolder, string targetFolder)
    Copies the entire folder tree from source to target
	
    .CopyFullToFlat(string sourceFolder, string targetFolder)
    Copies the full directory to a flat structure.  Use this whenever you want 
    flatten out the hierarchy
	
    CopyFullToFlat(string sourceFolder, string targetFolder, string[] fileExtensions)
    Copies full to flat by file extensions.  For example if you want to move all your
    .jpg files from  C:\Temp to an archivea at C:\MyPictures
    This would search C:\Temp and all of it's subdirectories recursively and copy all
    the pictures into the folder C:\MyPictures.  If a duplicate file name is found, for
    example one in C:\Temp\Child1\mypic.jpg and one in C:\Temp\Child2\mypic.jpg. Those
    pictures would end up as
    c:\MyPictures\mypic.jpg
    c:\MyPicutres\mypic(01).jpg
    
    using System;
    using DirectoryWiz.Framework.Api;

    namespace DirWizConsole
    {
        class Program
        {
            //Exampe Usage
            static void CopyExample()
            {

                GeneralFileCopier fileCopier = new GeneralFileCopier();
                fileCopier.ErrorOccurred += new EventHandler<FileActionEventArgs>(fileCopier_ErrorOccurred);
                fileCopier.OnProgress += new EventHandler<FileActionEventArgs>(fileCopier_OnProgress);

                //Copy the entire folder from C:\temp to D:\backup
                fileCopier.CopyFull("C:\\Temp\\", "D:\\Backup");
	  
                //Cooy all images from D:\Backup and its subfolders into the D:\Backup\Images
                //No subfolders will be created in the target folder
                //Any secondary files with same names will have a number appended to them 
                //Example:   D:\BackupImages\mypic.jpg D:\BackupImages\mypic(1).jpg
                fileCopier.CopyFullToFlat("D:\\Backup", "D:\\Backup\\images", new string[] { ".bmp", ".png", ".gif", ".bmp" });

            }

            static void fileCopier_OnProgress(object sender, FileActionEventArgs e)
            {
                Console.WriteLine(e.Message);
            }
			
            static void fileCopier_ErrorOccurred(object sender, FileActionEventArgs e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

        
	
************************************
* Command Line Usage               *
************************************

*********** Help **********************
div --help

	
*********** Remove Examples ***********


To Remove by extension
div --remove -e "C:\path\to\myfolder\" ".jpg .png .bmp .jpeg"

To Remove by file name
div --remove -n "C:\path\to\myfolder\" "myfile.bmp otherfile.txt thirdfile.doc"

To Remove by folder name
div --remove -fn "C:\path\to\myfolder\" "bin debug .svn" 

To Remove by regex
div --remove -rx "C:\path\to\myfolder\" "[A-Z][1-9]" (Not implemented yet)

*********** Copy Examples (Not Yet Implemented) ***********

To Copy full tree
div --copy -fu "C:/my/source/path" "C:/my/target/path"

To copy folders only
div --copy -em "C:/my/source/path" "C:/my/target/path"

To copy full tree to flat hierarchy
div --copy -fl "C:/my/source/path" "C:/my/target/path"

To copy full tree to flat hierarchy by file extensions
div --copy -fl "C:/my/source/path" "C:/my/target/path" ".jpg .png .img"

To copy full tree to flat hierarchy by ignoring certain file extensions
div --copy -fi "C:/my/source/path" "C:/my/target/path" ".jpg .png .img"

To Copy by regex any full file path that matches the expression
div --copy -fl -rx "[A-Z][1-9]" (Not implemented yet)

		