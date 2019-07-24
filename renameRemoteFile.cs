// This example requires the Chilkat API to have been previously unlocked.
// See Global Unlock Sample for sample code.

// https://docs.microsoft.com/en-us/dotnet/api/system.net.ftpwebrequest.timeout?view=netframework-4.8
// Need to install Chilkat API

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace rename
{
    class renameRemoteFile
    {
    
        // rename file from directory. Takes first argument from command line and 
        // checks if valid input, renames file from directory and outputs success message
        static void Main(string[] args)
        {
            String[] arguments = Environment.GetCommandLineArgs();
            String oldFile = arguments[1];
            String newFile = arguments[2];

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            try
            {
                if (File.Exists(path))
                {
                    Chilkat.Ftp2 ftp = new Chilkat.Ftp2();

                    ftp.Hostname = "ftp.example.com";
                    ftp.Username = "login";
                    ftp.Password = "password";

                    // Connect and login to the FTP server.
                    bool success = ftp.Connect();
                    if (success != true) {
                        Debug.WriteLine(ftp.LastErrorText);
                        return;
                    }

                    // Set the current remote directory to where the file
                    // is located:
                    success = ftp.ChangeRemoteDir(path);
                    if (success != true) {
                        Debug.WriteLine(ftp.LastErrorText);
                        return;
                    }

                    // Rename the remote file (or directory)
                    
                    success = ftp.RenameRemoteFile(oldFile,newFile);
                    if (success != true) {
                        Debug.WriteLine(ftp.LastErrorText);
                        return;
                    }

                    success = ftp.Disconnect();
                    Console.WriteLine("\nProcess complete: \"" + oldFile + "\" deleted \""  + newFile);
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