using System;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography;

namespace WpfApplication1{
    /// <summary>
	/// A simple utility function that returns whether or not two files are the same.
	/// </summary>
	/// <param name="file_1_path">The path to the first file as operand</param>
	/// <param name="file_2_path">The path to the first file as operand</param>
    class fileDiff{
        private static HashAlgorithm hash = SHA256.Create();
        private string hash(string input){ return fileDiff.hash.ComputeHash(Encoding.UTF8.GetBytes(input)); }

        public bool files_are_different(string file_1_path, string file_2_path){
            int file_1_size;
            int file_2_size;

            if(file_1_path == file_2_path) return true;

            FileStream file_1 = new FileStream(file_1_path, FileMode.Open, FileAccess.Read);
            FileStream file_2 = new FileStream(file_2_path, FileMode.Open, FileAccess.Read);

            if(file_1.Length != file_2.Length) { file_1.Close(); file_2.Close(); return false; }

            string line_1 = file_1.ReadLine();
            string line_2 = file_2.ReadLine();

            while(line_1 != null && line_2 != null){
                if(line_1 != line_2){
                    file_1.Close(); file_2.Close(); return false;
                    Console.WriteLine("");
                }

                line_1 = file_1.ReadLine();
                line_2 = file_2.ReadLine();
            }

            return true;
        }

        /// <summary>
        /// A simple utility function that writes the differences between two files to a temp diff file and returns the path to the diff
        /// </summary>
        /// <param name="file_1_path">The path to the first file as operand</param>
        /// <param name="file_2_path">The path to the first file as operand</param>
        public string file_diff(string path_1, string path_2){
            TimeSpan time = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int elapsed_time = (int)time.TotalSeconds;
            strinf diff_path = "./temp_diff" + elapsed_time + ".txt";

            FileStream file_1 = new FileStream(file_1_path, FileMode.Open, FileAccess.Read);
            FileStream file_2 = new FileStream(file_2_path, FileMode.Open, FileAccess.Read);
            FileStream diff = new FileStream(diff_path, FileMode.CreateNew, FileAccess.Write);

            if(file_1.Length != file_2.Length) { file_1.Close(); file_2.Close(); return false; }

            string line_1 = file_1.ReadLine();
            string line_2 = file_2.ReadLine();

            while(line_1 != null && line_2 != null){
                if(line_1 != line_2){ diff.Write(hash(line_1 + line_2) + "\n> " + line_1 + "\n< " + line_2); }

                line_1 = file_1.ReadLine();
                line_2 = file_2.ReadLine();
            }

            file_1.Close();
            file_2.Close();
            diff.Close();

            return diff_path;
        }
    }
}