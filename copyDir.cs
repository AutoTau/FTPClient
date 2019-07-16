using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyDir
{
    class CopyDir
    {
        // From the command line, run the exectuable and add destination directory as arg1.
        
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            // unnecessary for now, since the current directory will exist
            // but keeping this just in case the functionality for specifying 
            // a different directory is desired.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found. "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        static void Main(string[] args)
        {

            String[] arguments = Environment.GetCommandLineArgs();
            String path = arguments[1];

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            // copy this dir to location "path".
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("\ndirectory already exists.");
                }
                else
                {
                    // Copy from the current directory, include subdirectories.
                    DirectoryCopy(".", path, true);
                    // Print to console the results.
                    Console.WriteLine("Current directory copied to: " + path);
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("{0} is not a valid file or directory.", e);
            }
        }
    }
    
}
