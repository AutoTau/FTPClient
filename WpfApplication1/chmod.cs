using System;
using System.Net;

namespace WpfApplication1
{
	/// <summary>
	/// As per: https://github.com/robinrodricks/FluentFTP/blob/master/FluentFTP/Client/FtpClient_FileManagement.cs
	/// Modify the permissions of the given file/folder.
	/// Only works on *NIX systems, and not on Windows/IIS servers.
	/// Only works if the FTP server supports the SITE CHMOD command
	/// (requires the CHMOD extension to be installed and enabled).
	/// Throws FtpCommandException if there is an issue.
	/// </summary>
	/// <param name="Path">The full or relative path to the item</param>
	/// <param name="Permissions">The permissions in CHMOD format</param>
	/// <param name="HostName">The permissions in CHMOD format</param>
	/// <param name="UserName">The permissions in CHMOD format</param>
	/// <param name="Password">The permissions in CHMOD format</param>
    public class chmod 
    { 
           public void changePermissions(string Path, string Permissions, string HostName, string UserName, string Password)
           {
               FluentFTP.FtpClient client = new FluentFTP.FtpClient(HostName);
               client.Credentials = new NetworkCredential(UserName, Password);
               client.Connect();
               client.Chmod(Path, Convert.ToInt32(Permissions));
               client.Disconnect();
           } 
    }
}