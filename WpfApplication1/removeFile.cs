// David Djernaes
// CS410 Agile group project

using System;
using System.IO;
using System.Net;



namespace WpfApplication1
{
    public class removeFile
    {

        // remove file from directory. Takes first argument from command line and 


        /// <summary>
        /// Deletes the file the user selects
        /// </summary>
        /// <param name="HostName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="FileToDelete"></param>

        public void DeleteFile(string HostName, string UserName, string Password, string FileToDelete)
        {
            
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{FileToDelete}")));
            
            request.Credentials = new NetworkCredential(UserName, Password);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            System.IO.FileInfo file = new System.IO.FileInfo(FileToDelete);
            try
            {
                if (File.Exists(FileToDelete))
                {
                    file.Delete();
                    Console.WriteLine("\nProcess complete: \"" + FileToDelete + "\" deleted");
                }
                else
                {
                    Console.WriteLine("file not found");
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("{0} is not a valid file or directory.", e);
            }
        }
    }
}
