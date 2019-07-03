using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace WpfApplication1
{
    public class FTPClientViewModel : ObservableItem
    {

        FTPClientModel ClientModel;
        public ICommand UploadFile { get; private set; }
        public ICommand SelectFile { get; private set; }

        public FTPClientViewModel()
        {
            ClientModel = new FTPClientModel();
            this.UploadFile = new Command(ced => true, ed => ClientModel.UploadSelectedFile(HostName,UserName,Password,FileToUpload,Port));
            this.SelectFile = new Command(ced => true, ed => this.InitiateDialogBox());

        }

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

    }
}
