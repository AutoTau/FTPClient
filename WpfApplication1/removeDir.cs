// David Djernaes
// CS410 Agile group project

using System;
using System.IO;
using System.Net;

namespace WpfApplication1
{

    public class removeDir
    {

        
        // Process directory passed in as argument
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }



        /// <summary>
        /// Deletes the directory the user selects
        /// </summary>
        /// <param name="HostName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DirectoryToDelete"></param>
        /// 
        public void DeleteDirectory(string HostName, string UserName, string Password, string DirectoryToDelete)
        {
            
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{DirectoryToDelete}")));
            request.Credentials = new NetworkCredential(UserName, Password);

            try
            {
                if (Directory.Exists(DirectoryToDelete))
                {
                    request.Method = WebRequestMethods.Ftp.RemoveDirectory; 
                    Console.WriteLine("\nProcess complete: \"" + DirectoryToDelete + "\" deleted");
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
        }

       
    }
}



