using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _customers;
        private ICustomersRepository _repo = new CustomersRepository();
        public RelayCommand DeleteCommand { get; private set; }
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
                }
            }
        }

        public CustomerListViewModel()
        {
            
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }
        public async void LoadCustomers()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return; //we use this line so that the call to data won't execute in the designer
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());

        }
        private Customer _selectedCustomer;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
                }               
            }
        }
        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }
    }
}
