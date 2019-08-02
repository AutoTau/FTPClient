using System;
using System.IO;
using System.Text;
using System.Net;

namespace CopyDir
{
    class CopyDir
    {
        // Copy directory contents from remote host to local directory
        // makes a list of source directory files and subdirectory files, if they exist
        // copies files to user designated destination directory
        
        private static void DirectoryCopy(string HostName, string UserName, string Password, string SourceDirName, string DestDirName)
        {
            
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format($"ftp://{HostName}" + $"/{SourceDirName}")));


            string[] files = GetFileList();
            foreach (string file in files)
            {
                Download(file);
            }

            string[] GetFileList()
            {
                string[] downloadFiles;
                StringBuilder result = new StringBuilder();
                WebResponse response = null;
                StreamReader reader = null;
                try
                {
                    request.UseBinary = true;
                    request.Credentials = new NetworkCredential(UserName, Password);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Proxy = null;
                    request.KeepAlive = false;
                    request.UsePassive = false;
                    response = request.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    // to remove the trailing '\n'
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    return result.ToString().Split('\n');
                }
                catch (Exception ex)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    if (response != null)
                    {
                        response.Close();
                    }
                    Console.WriteLine(ex);
                    downloadFiles = null;
                    return downloadFiles;
                }
            }

            void Download(string file) 
            {
                try
                {
                    string uri = ($"ftp://{HostName}" + $"/{SourceDirName}" + $"/{file}");
                    Uri serverUri = new Uri(uri);
                    if (serverUri.Scheme != Uri.UriSchemeFtp)
                    {
                        return;
                    }
                    FtpWebRequest reqFTP;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri($"ftp://{HostName}" + $"/{SourceDirName}" + $"/{file}"));
                    reqFTP.Credentials = new NetworkCredential(UserName, Password);
                    reqFTP.KeepAlive = false;
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Proxy = null;
                    reqFTP.UsePassive = false;
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    FileStream writeStream = new FileStream(DestDirName + "\"" + file, FileMode.Create);                
    
                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);
                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, Length);
                    }
                    writeStream.Close();
                    response.Close();
                }
                catch (WebException wEx)
                {
                    //MessageBox.Show(wEx.Message, "Download Error");
                    Console.WriteLine("Download error" + wEx);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Download Error");
                    Console.WriteLine("Download error" + ex);
                }
            }

        }

    }
    
}
