using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Zza.Data;
using ZzaDashboard.Services;
using ZzaDesktop;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;       
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                SetProperty(ref _customers, value);
            }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public event Action<Guid> PlaceOrderRequested = delegate { };

        public RelayCommand AddCustomerCommand { get; private set; }
        public event Action<Customer> AddCustomerRequested = delegate { };

        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public event Action<Customer> EditCustomerRequested = delegate { };

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }

        private void OnEditCustomer(Customer customer)
        {
            EditCustomerRequested(customer);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer { Id = Guid.NewGuid() });
        }

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }

        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(
                await _repo.GetCustomersAsync());
        }
       
    }
}
