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
        public void UploadSelectedFile(string HostName, string UserName, string Password,string FileToUpload, int Port)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", HostName, FileToUpload)));
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
    }
}
