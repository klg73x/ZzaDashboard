﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zza.Data
{
    public class Customer : INotifyPropertyChanged
    {
        public Customer()
        {
            Orders = new List<Order>();
        }
        [Key]
        public Guid Id { get; set; }
        public Guid? StoreId { get; set; }
        private string _firstName;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
                }

            }
        }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<Order> Orders { get; set; }
    }
}
