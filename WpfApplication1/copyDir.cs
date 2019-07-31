using System;
using System.IO;
using System.Net;


namespace WpfApplication1
{
    class CopyDir
    {
        // copy directory from remote location and copy to local directory.        
        
        private static void DirectoryCopy(string HostName, string DirectoryToCopy, string DestinationDirectory, bool copySubDirs)
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{DirectoryToCopy}")));

            try
            {
                if (Directory.Exists(DirectoryToCopy))
                {
                    request.Method = WebRequestMethods.Ftp.CopyDirectory; 
                    Console.WriteLine("\nProcess complete: \"" + DirectoryToCopy + "\" copied");
                }
                else
                {
                    Console.WriteLine("directory not found");
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("{0} is not a valid file or directory.", e);

            }

            
            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(DestinationDirectory))
            {
                Directory.CreateDirectory(DestinationDirectory);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(DestinationDirectory, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(DestinationDirectory, subdir.Name);
                    DirectoryCopy(HostName, DirectoryToCopy, DestinationDirectory, copySubDirs);
                }
            }

            // copy remote directory to destination location 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(DestinationDirectory);
            // copy this dir to location "path".
            try
            {
                if (Directory.Exists(DestinationDirectory))
                {
                    Console.WriteLine("\ndirectory already exists.");
                }
                else
                {
                    // Copy from the current directory, include subdirectories.
                    DirectoryCopy(HostName, DirectoryToCopy, ".", true);
                    // Print to console the results.
                    Console.WriteLine("Current directory copied to: " + DestinationDirectory);
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("{0} is not a valid file or directory.", e);
            }
        }
        
    }
    
}
