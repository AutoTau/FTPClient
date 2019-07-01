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
    }
}
