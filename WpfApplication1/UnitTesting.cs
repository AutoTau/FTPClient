using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    using FakeItEasy;
    using NUnit;
    using NUnit.Framework;

    public class UnitTesting
    {
        string hostname = "localhost";
        int port = 8008;
        string username = "admin";
        string password = "admin";
        string filename = "testFile.txt";

        List<string> files_to_upload = new List<string>();
        List<string> files_to_download = new List<string>();


        /// <summary>
        /// Tests the expected path through the code with correct paths/inputs
        /// </summary>
        [Test]
        public void testingUpload()
        {
            var uploadresult = A.Fake<FTPClientModel>();
            files_to_upload.Add(filename);
            files_to_upload.Add(filename);

            try
            {
                var returnval = uploadresult.UploadSelectedFile(hostname, username, password, files_to_upload, port, false);
                if (returnval == false)
                    throw new Exception("UploadSelectedFile returned false!!!");
                else
                    Console.WriteLine("testingUpload passed. UploadSelectedFile returned as expected");

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}... UploadSelectedFile has FAILED unit test");
            }

        }

        /// <summary>
        /// Expecting UploadSelectedFile to throw an exception when passing in an invalid hostname
        /// </summary>
        [Test]
        public void testingUploadInvalidHostname()
        {
            string invalidhostname = "blablabla";
            var uploadresult = A.Fake<FTPClientModel>();
            files_to_upload.Add(filename);
            files_to_upload.Add(filename);

            try
            {
                var returnval = uploadresult.UploadSelectedFile(invalidhostname, username, password, files_to_upload, port, false);
                if (returnval == true)
                    throw new Exception("UploadSelectedFile returned true, expected false!!!!");
                else
                    Console.WriteLine("testingUploadInvalidHostname passed. UploadSelectedFile returned as expected when passed an invalid hostname.");

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}... UploadSelectedFile has FAILED unit test");
            }

        }


        /// <summary>
        /// Tests the expected path through the code with correct paths/inputs
        /// </summary>
        [Test]
        public void testingDownload()
        {

            var downloadResult = A.Fake<FTPClientModel>();
            files_to_download.Add(filename);
            files_to_download.Add(filename);

            try
            {
                var returnval = downloadResult.DownloadSelectedFile(hostname, username, password, files_to_download, port);
                if (returnval == false)
                    throw new Exception("DownloadSelectedFile returned false!!!");
                else
                    Console.WriteLine("testingdownload passed. DownloadSelectedFile returned as expected. PASS");

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}... DownloadSelectedFile has FAILED unit test");
            }

        }


        /// <summary>
        /// Expecting UploadSelectedFile to throw an exception when passing in an invalid hostname
        /// </summary>
        [Test]
        public void testingDownloadInvalidHostname()
        {
            string invalidhostname = "blablabla";
            var downloadresult = A.Fake<FTPClientModel>();
            files_to_download.Add(filename);
            files_to_download.Add(filename);

            try
            {
                var returnval = downloadresult.UploadSelectedFile(invalidhostname, username, password, files_to_download, port, false);
                if (returnval == true)
                    throw new Exception("downloadSelectedFile returned true, expected false!!!!");
                else
                    Console.WriteLine("testingDownloadInvalidHostname passed. DownloadSelectedFile returned as expected when passed an invalid hostname.");

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}... UploadSelectedFile has FAILED unit test");
            }

        }

        /// <summary>
        /// Main routine for testing the functions
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            UnitTesting unitTests = new UnitTesting();

            Console.WriteLine("Testing the function UploadSelectedFile()");
            unitTests.testingUpload();

            Console.WriteLine("Testing the function UploadSelectedFile() with an invalid hostname");
            unitTests.testingUploadInvalidHostname();

            Console.WriteLine("Testing the function DownloadSelectedFile()");
            unitTests.testingDownload();

            Console.WriteLine("Testing the function DownloadSelectedFile() with an invalid hostname");
            unitTests.testingDownloadInvalidHostname();

        }

    }
}
