using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Net;

namespace WpfApplication1
{
    public class FTPClientViewModel : ObservableItem
    {

        FTPClientModel ClientModel;
        removeFile RemoveCertainFile;
        removeDir RemoveCertainDirectory;

        public ICommand UploadFile { get; private set; }
        public ICommand SelectFileToUpload { get; private set; }
        public ICommand SelectFileToDownload { get; private set; }
        public ICommand DownloadFile { get; private set; }
        public ICommand RemoveFile { get; private set; }
        public ICommand RemoveDirectory { get; private set; }
        public ICommand LogOffFromRemote { get; private set; }
        public Command CopyDirectory { get; }

        //private BackgroundWorker _bgWorker = new BackgroundWorker();

        /// <summary>
        /// Toggle on and off or hide the progress bar
        /// </summary>        
        public Visibility ProgressBarVisiblity { get; private set; }


        public FTPClientViewModel()
        {
            ProgressBarVisiblity = Visibility.Collapsed;
            ClientModel = new FTPClientModel();
            RemoveCertainFile = new removeFile();
            RemoveCertainDirectory = new removeDir();

            this.UploadFile = new Command(ced => true, ed => ClientModel.UploadSelectedFile(HostName,UserName,Password,FileToUpload,Port,false));
            this.SelectFileToUpload = new Command(ced => true, ed => this.InitiateDialogBox());
            this.SelectFileToDownload = new Command(ced => true, ed => this.SelectFileFromFtpServer());
            this.DownloadFile = new Command(ced => true, ed => this.ClientModel.DownloadSelectedFile(HostName, UserName, Password, FileToDownload, Port, false));
            this.RemoveFile = new Command(ced => true, ed => RemoveCertainFile.DeleteFile(HostName, PathOfFileToRemove));
            this.RemoveDirectory = new Command(ced => true, ed => RemoveCertainDirectory.DeleteDirectory(HostName, PathOfFileToRemove));
            this.LogOffFromRemote = new Command(ced => true, ed => ClientModel.UploadSelectedFile(HostName, UserName, Password, FileToUpload, Port, true));
            this.CopyDirectory = new Command(ced => true, ed => CopyCertainDirectory.DirectoryCopy(HostName, UserName, Password, SourceDirName, DestDirName));
            ClientModel.ToggleProgressBar += FTPClientModel_ToggleProgressBar;
        }


        /// <summary>
        /// Toggles the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FTPClientModel_ToggleProgressBar(object sender, bool e)
        {
            ProgressBarVisiblity = e ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged(() => ProgressBarVisiblity);
        }


        /// <summary>
        /// Gets or sets the hostname.
        /// </summary>
        private string _hostName = string.Empty;
        public string HostName
        {
            get => _hostName;
            set
            {
                _hostName = value;
                this.OnPropertyChanged(() => this.HostName);
            }
        }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                this.OnPropertyChanged(() => this.UserName);
            }
        }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                this.OnPropertyChanged(() => this.Password);
            }
        }
        
        /// <summary>
        /// Gets or sets the Port number.
        /// </summary>
        private int _port = 0;
        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                this.OnPropertyChanged(() => this.Port);
            }
        }

        /// <summary>
        /// Gets or sets the File to be uploaded.
        /// </summary>
        private string _fileToUpload = string.Empty;
        public string FileToUpload
        {
            get => _fileToUpload;
            set
            {
                _fileToUpload = value;
                this.OnPropertyChanged(() => this.FileToUpload);
            }
        }

        /// <summary>
        /// Gets or sets the GUI test field.
        /// </summary>
        private string _testField = string.Empty;
        public string TestField
        {
            get => _testField;
            set
            {
                _testField = value;
                this.OnPropertyChanged(() => this.TestField);
            }
        }

        /// <summary>
        /// Gets or sets the File to download.
        /// </summary>
        private string _fileToDownload = string.Empty;
        public string FileToDownload
        {
            get => _fileToDownload;
            set
            {
                _fileToDownload = value;
                this.OnPropertyChanged(() => this.FileToDownload);
            }
        }

        /// <summary>
        /// Gets or sets the Files or Directories to be removed.
        /// </summary>
        private string _pathOfFileToRemove = string.Empty;
        public string PathOfFileToRemove
        {
            get => _pathOfFileToRemove;
            set
            {
                _pathOfFileToRemove = value;
                this.OnPropertyChanged(() => this.PathOfFileToRemove);
            }
        }

        public object CopyCertainDirectory { get; }
        public object SourceDirName { get; }
        public object DestDirName { get; }

        /// <summary>
        /// File dialog to select the file to upload.
        /// Default path is the system desktop path.
        /// </summary>
        public void InitiateDialogBox()
        {
            string startDirectory = Environment.SpecialFolder.Desktop.ToString();

            var dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "All files (*.*)|*.*|All files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = startDirectory.ToString()
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileToUpload = dialog.FileName.ToString();
            }
        }

        /// <summary>
        /// File dialog to select the file to download.
        /// Default path is ftp://hostname
        /// </summary>
        public void SelectFileFromFtpServer()
        {
            string startDirectory = string.Format($"ftp://{HostName}");

            var dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "All files (*.*)|*.*|All files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = startDirectory.ToString()
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileToDownload = dialog.FileName.ToString();
            }
        }

    }
}
