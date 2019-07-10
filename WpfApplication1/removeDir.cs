// David Djernaes
// CS410 Agile group project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Collections;


namespace rmDir
{

    class removeDir
    {
        // Process directory passed in as argument
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }



        static void Main(string[] args)
        {
            String[] arguments = Environment.GetCommandLineArgs();
            String path = arguments[1];

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            // Delete this dir.
            try
            {
                if (Directory.Exists(path))
                {
                    dir.Delete(true);
                    Console.WriteLine("\nProcess complete: \"" + path + "\" deleted");
                }
                else
                {
                    Console.WriteLine("directory not found");
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("{0} is not a valid file or directory.", e);

            }



        }
    }
}



