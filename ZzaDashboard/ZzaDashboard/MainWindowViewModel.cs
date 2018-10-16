using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using ZzaDashboard.Customers;

namespace ZzaDashboard
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Timer _timer = new Timer(5000);

        public MainWindowViewModel()
        {
            CurrentViewModel = new CustomerListViewModel();
            _timer.Elapsed += (s, e) => NotificationMessage = "At the tone the time will be: " + DateTime.Now.ToString();
            _timer.Start();
        }

        public object CurrentViewModel { get; set; }

        private string _NotificationMessage;
        public string NotificationMessage
        {
            get
            {
                return _NotificationMessage;
            }
            set
            {
                if (value != _NotificationMessage)
                {
                    _NotificationMessage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
