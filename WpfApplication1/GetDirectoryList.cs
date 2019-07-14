using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// This code is loosely based on a tutorial from Packt publishing titled,
// "Hands-On Network Programming with C# and .NET Core"

namespace WpfApplication1
{
    public class GetDirectoryList
    {
        public async Task<string> GetDirectory(string HostName, string UserName, string Password, int Port) 
        {
            StringBuilder strBuilder = new StringBuilder();
            if (Port > 0) HostName += HostName + ":" + Port + "/";
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(HostName);
            req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            req.Credentials = new NetworkCredential(UserName, Password);
            req.EnableSsl = false;

            FtpWebResponse resp = (FtpWebResponse)await req.GetResponseAsync();

            using (var respStream = resp.GetResponseStream()) {
                using (var reader = new StreamReader(respStream)) {
                    strBuilder.Append(reader.ReadToEnd());
                    strBuilder.Append(resp.WelcomeMessage);
                    strBuilder.Append($"Request returned status:  {resp.StatusDescription}");
                }
            }
            return strBuilder.ToString();
        }
    }
}
