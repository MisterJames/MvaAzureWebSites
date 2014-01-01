// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace CustomerManager.StyleUI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CustomerApi.Models.WebApi.Models;
    using CustomerManager.StyleUI.Common;
    using CustomerManager.StyleUI.DataModel;

    public class CustomerDetailViewModel : BindableBase
    {
        private int _selectedCustomerId;
        public int SelectedCustomerId
        {
            get
            {
                return this._selectedCustomerId;
            }
            set
            {
                this.SetProperty(ref this._selectedCustomerId, value);
                this.OnPropertyChanged("SelectedCustomer");
            }
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get
            {
                return this._selectedCustomer;
            }
            set
            {                
                this.SetProperty(ref this._selectedCustomer, value);                
            }

        }

        public ObservableCollection<CustomerViewModel> CustomersList { get; set; }

        public CustomerDetailViewModel()
        {
            this.CustomersList = new ObservableCollection<CustomerViewModel>();

            this.GetCustomers();
        }

        private async void GetCustomers()
        {
            IEnumerable<Customer> customers = await CustomersWebApiClient.GetCustomers();
            foreach (var customer in customers)
            {                
                this.CustomersList.Add(new CustomerViewModel(customer));                
            }

            this.SelectedCustomer = this.CustomersList.FirstOrDefault(c => c.CustomerId == this.SelectedCustomerId);
        }
    }
}
