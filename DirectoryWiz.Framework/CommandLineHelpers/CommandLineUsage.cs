﻿using System;

namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public class CommandLineUsage
    {
        public static string RemoveUsage =
            "*********** Remove Examples ***********" + Environment.NewLine + Environment.NewLine
            + "  " + Environment.NewLine
            + "To Remove by extension" + Environment.NewLine
            + "div --remove -e \"C:\\path\\to\\myfolder\\\" \".jpg .png .bmp .jpeg\"" + Environment.NewLine
            + " " + Environment.NewLine
            + "To Remove by file name" + Environment.NewLine
            + "div --remove -n \"C:\\path\\to\\myfolder\\\" \"myfile.bmp otherfile.txt thirdfile.doc\"" + Environment.NewLine 
            + "To Remove by folder name" + Environment.NewLine
            + "div --remove -fn \"C:\\path\\to\\myfolder\\\" \"bin debug obj .svn\"" + Environment.NewLine
            + " " + Environment.NewLine
            + "To Remove by regex" + Environment.NewLine
            + "div --remove -rx \"C:\\path\\to\\myfolder\\\" \"[A-Z][1-9]\" (Not implemented yet)" + Environment.NewLine
            + " " + Environment.NewLine;
            
        public static string CopyUsage =
              "*********** Copy Examples ***********" + Environment.NewLine + Environment.NewLine
            + "To Copy full tree" + Environment.NewLine
            + "div --copy -fu \"C:/my/source/path\" \"C:/my/target/path\"" + Environment.NewLine

            + " " + Environment.NewLine
            + "To copy folders only" + Environment.NewLine
            + "div --copy -fo \"C:/my/source/path\" \"C:/my/target/path\"" + Environment.NewLine
            + " " + Environment.NewLine
            
            + "To copy full tree to flat hierarchy" + Environment.NewLine
            + "div --copy -ff \"C:/my/source/path\" \"C:/my/target/path\"" + Environment.NewLine
            
            + " " + Environment.NewLine
            
            + "To copy full tree to flat hierarchy by file extensions" + Environment.NewLine
            + "div --copy -ffe \"C:/my/source/path\" \"C:/my/target/path\" \".jpg .png .img\"" + Environment.NewLine
            + " " + Environment.NewLine
            
            + "To copy full tree to flat hierarchy by ignoring certain file extensions" + Environment.NewLine
            + "div --copy -ffi \"C:/my/source/path\" \"C:/my/target/path\" \".jpg .png .img\"" + Environment.NewLine
            + " " + Environment.NewLine
            
            + "To Copy by regex any full file path that matches the expression" + Environment.NewLine
            + "div --copy -fl -rx \"[A-Z][1-9]\" (Not implemented yet)" + Environment.NewLine
            + " " + Environment.NewLine;

        public static string Usage =
              RemoveUsage
            + CopyUsage;
    }
}
