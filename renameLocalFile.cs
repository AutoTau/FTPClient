// Parker Moore
// CS410 Agile group project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace renameLocalFile
{
    class rename
    {
    
        // remove file from directory. Takes first argument from command line and 
        // checks if valid input, deletes file from directory and outputs success message
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
                    File.Move(oldFile,newFile);
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
