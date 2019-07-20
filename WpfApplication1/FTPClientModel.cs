using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using MahApps.Metro.Controls;

namespace WpfApplication1
{
    public class FTPClientModel
    {
        /// <summary>
        /// Struct for a single instance of saved login information
        /// <summary>
        /// <param name="HostName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Port"></param>
        private struct userLog
        {
            public string HostName;
            public string UserName;
            public string Password;
            public int Port;
        }
        /// <summary>
        /// List for every instance of saved login information
        /// </summary>
        private List<userLog> userLogHistory;

        /// <summary>
        /// Read plain text user information
        /// <summary>
        private List<userLog> readUserLogHistory()
        {
            userLog userInfo = new userLog();
            List<userLog> userLogHist = new List<userLog>();
            
            string[] lines = File.ReadAllLines( Directory.GetCurrentDirectory() + "/userHistory.txt" );
            foreach (string line in lines)
            {
                string[] col = line.Split( new char[] {','} );
                userInfo.HostName = col[0];
                userInfo.UserName = col[1];
                userInfo.Password = col[2];
                userInfo.Port     = Int32.Parse(col[3]);
                userLogHistory.Add(userInfo);
            }
            return userLogHist;
        }
        
        /// <summary>
        /// Create a queue of files for upload/download
        /// </summary>
        public Queue<string> buildFileQueue(params string[] files)
        {
            Queue<string> fileQueue = new Queue<string>();
            for (int i = 0; i <= files.Length; i++)
            {
                fileQueue.Enqueue(files[i]);
            }
            return fileQueue;
        }
    
        
        public event EventHandler<bool> ToggleProgressBar;
        /// <summary>
        /// Default constructor, sets ToggleProgressBar to false initially
        /// </summary>
        public FTPClientModel()
        {
            ToggleProgressBar?.Invoke(this, false);
        }

        /// <summary>
        /// Uploads the file the user selects
        /// </summary>
        /// <param name="HostName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="FileToUpload"></param>
        /// <param name="Port"></param>
        public void UploadSelectedFile(string HostName, string UserName, string Password, string FileToUpload, int Port)
        {
            double percentage = 0;
            try
            {
                ToggleProgressBar?.Invoke(this, true);
                string file = Path.GetFileName(FileToUpload);
                FtpWebRequest request =
                    (FtpWebRequest) WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{file}")));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                Stream ftpStream = request.GetRequestStream();
                FileStream fStream = File.OpenRead(FileToUpload);
                byte[] buffer = new byte[1024];
                int byteRead = 0;
                double read = 0;
                double size = (double) fStream.Length;

                do
                {
                    byteRead = fStream.Read(buffer, 0, 1024);
                    ftpStream.Write(buffer, 0, byteRead);
                    read += (double) byteRead;
                    percentage = read / size * 100; //Progress percentage.
                } while (byteRead != 0);

                fStream.Close();
                ftpStream.Close();
                ToggleProgressBar?.Invoke(this, false);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace, e?.InnerException);
                ToggleProgressBar?.Invoke(this, false);
            }
        }


        /// <summary>
        /// Downloads the file the user selects
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="fileToDownload"></param>
        /// <param name="port"></param>
        public void DownloadSelectedFile(string hostName, string userName, string password, string fileToDownload,
            int port)
        {
            try
            {
                ToggleProgressBar?.Invoke(this, true);
                var request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{hostName}/{fileToDownload}")));
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(userName, password);

                var response = (FtpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine($"Download complete. Status: {response.StatusDescription}");

                reader.Close();
                response.Close();
                ToggleProgressBar?.Invoke(this, false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception thrown in DownloadSelectedFile(): {e.Message}, {e.StackTrace}");
                ToggleProgressBar?.Invoke(this, false);
            }
        }


        /// <summary>
        /// Downloads the sequence of file the user selects
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="filesToDownload"></param>
        /// <param name="fileQueue"></param>
        /// <param name="port"></param>
        public void DownloadSelectedFiles(string hostName, string userName, string password, Queue<string> fileQueue,
            int port)
        {
            while (fileQueue.Count != 0)
            {
                string file = fileQueue.Dequeue();
                DownloadSelectedFile(hostName, userName, password, file, port);
            }
        }
        
        /// <summary>
        /// Returns a string of file names from directory,
        /// following Microsoft's online documentation
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public string GetDirectory(string hostName, string userName, string password)
        {
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create($"ftp://{hostName}/");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            request.Credentials = new NetworkCredential(userName, password);

            FtpWebResponse response = (FtpWebResponse) request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            string directoryInfo = response.StatusDescription;

            reader.Close();
            response.Close();
            return directoryInfo;
        }
        
        
    }
}
