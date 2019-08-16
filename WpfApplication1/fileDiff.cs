using System;
using System.IO;
using System.Text;
using System.Net;

namespace WpfApplication1{
    /// <summary>
	/// A simple utility function that returns whether or not two files are the same.
	/// </summary>
	/// <param name="file_1_path">The path to the first file as operand</param>
	/// <param name="file_2_path">The path to the first file as operand</param>
    class fileDiff{
        public bool fileDiff(string file_1_path, string file_2_path){
            int file_1_size;
            int file_2_size;

            if(file_1_path == file_2_path) return true;

            FileStream file_1 = new FileStream(file_1_path, FileMode.Open, FileAccess.Read);
            FileStream file_2 = new FileStream(file_2_path, FileMode.Open, FileAccess.Read);

            if(file_1.Length != file_2.Length) { file_1.Close(); file_2.Close(); return false; }

            string line_1 = file_1.ReadLine();
            string line_2 = file_2.ReadLine();

            while(line_1 != null && line_2 != null){
                if(line_1 != line_2){ file_1.Close(); file_2.Close(); return false; }

                line_1 = file_1.ReadLine();
                line_2 = file_2.ReadLine();
            }

            return true;
        }
    }
}