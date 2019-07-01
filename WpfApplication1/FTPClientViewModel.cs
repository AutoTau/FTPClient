using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class FTPClientViewModel : ObservableItem
    {

        FTPClientModel ClientModel;

        public FTPClientViewModel()
        {
            ClientModel = new FTPClientModel();
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

    }
}
