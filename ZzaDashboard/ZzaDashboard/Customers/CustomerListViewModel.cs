using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel
    {
        private ICustomersRepository _repo = new CustomersRepository();

        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return; //we use this line so that the call to data won't execute in the designer
            Customers = new ObservableCollection<Customer>(_repo.GetCustomersAsync().Result); //this .Result is needed because we are calling this syncronously even though it is an async call
        }

        public ObservableCollection<Customer> Customers { get; set; }
    }
}
