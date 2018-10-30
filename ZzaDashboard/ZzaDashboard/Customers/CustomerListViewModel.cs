﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Zza.Data;
using ZzaDashboard.Services;
using ZzaDesktop;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo;
        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }

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

        private string _SearchInput;
        private List<Customer> _allCustomers;

        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                SetProperty(ref _SearchInput, value);
                FilterCustomers(_SearchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => 
                    c.FullName.ToLower().Contains(searchInput.ToLower())));
            }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public event Action<Guid> PlaceOrderRequested = delegate { };

        public RelayCommand AddCustomerCommand { get; private set; }
        public event Action<Customer> AddCustomerRequested = delegate { };

        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public event Action<Customer> EditCustomerRequested = delegate { };

        public RelayCommand ClearSearchCommand { get; private set; }

       

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
            _allCustomers = await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(_allCustomers);
        }
       
    }
}
