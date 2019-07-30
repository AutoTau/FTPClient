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
//using MahApps.Metro.Controls;

namespace WpfApplication1
{
    public class FTPClientModel
    {
        
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
        public void UploadSelectedFile(string HostName, string UserName, string Password, List<string> FileToUpload, int Port, bool AbortClient)
        {
			foreach (var singleFile in FileToUpload)
			{

				double percentage = 0;
				try
				{
					ToggleProgressBar?.Invoke(this, true);
					string file = Path.GetFileName(singleFile);
					FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{file}")));
					if (AbortClient == true)
					{
						request.Abort();
					}
					FtpWebRequest request1 =
						(FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{file}")));
					request1.Method = WebRequestMethods.Ftp.UploadFile;
					request1.Credentials = new NetworkCredential(UserName, Password);
					Stream ftpStream = request1.GetRequestStream();
					FileStream fStream = File.OpenRead(singleFile);
					byte[] buffer = new byte[1024];
					int byteRead = 0;
					double read = 0;
					double size = (double)fStream.Length;

					do
					{
						byteRead = fStream.Read(buffer, 0, 1024);
						ftpStream.Write(buffer, 0, byteRead);
						read += (double)byteRead;
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
		}


		/// <summary>
		/// Downloads the file the user selects
		/// </summary>
		/// <param name="hostName"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="fileToDownload"></param>
		/// <param name="port"></param>
		public void DownloadSelectedFile(string hostName, string userName, string password, List<string> fileToDownload,
            int port)
        {
			foreach (var singleFile in fileToDownload)
			{
				try
				{
					ToggleProgressBar?.Invoke(this, true);
					var request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{hostName}/{singleFile}")));
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
        }

        public void LogOffFromServer()
        {

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
