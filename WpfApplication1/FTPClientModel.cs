using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WpfApplication1
{
    public class FTPClientModel
    {
        /// <summary>
        /// Uploads the file the user selects
        /// </summary>
        /// <param name="HostName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="FileToUpload"></param>
        /// <param name="Port"></param>
        public void UploadSelectedFile(string HostName, string UserName, string Password,string FileToUpload, int Port)
        {
            try
            {
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", HostName, FileToUpload)));
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}")));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                Stream ftpStream = request.GetRequestStream();
                FileStream fStream = File.OpenRead(FileToUpload);
                byte[] buffer = new byte[1024];
                int byteRead = 0;
                double read = 0;
                do
                {
                    byteRead = fStream.Read(buffer, 0, 1024);
                    ftpStream.Write(buffer, 0, byteRead);
                    read += (double)byteRead;
                }
                while (byteRead != 0);
                fStream.Close();
                ftpStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace, e?.InnerException);
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
        public void DownloadSelectedFile(string hostName, string userName, string password, string fileToDownload, int port)
        {
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
        }

    }
}
