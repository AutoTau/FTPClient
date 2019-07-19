// David Djernaes
// CS410 Agile group project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace WpfApplication1
{
    public class removeFile
    {
    
        // remove file from directory. Takes first argument from command line and 
        // checks if valid input, deletes file from directory and outputs success message
        public void DeleteFile(string path)
        {
            //String[] arguments = Environment.GetCommandLineArgs();
            //String path = arguments[1];

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            try
            {
                if (File.Exists(path))
                {
                    file.Delete();
                    Console.WriteLine("\nProcess complete: \"" + path + "\" deleted");
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
