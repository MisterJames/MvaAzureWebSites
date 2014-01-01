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
    using CustomerApi.Models.WebApi.Models;
    using CustomerManager.StyleUI.Common;
    using CustomerManager.StyleUI.DataModel;


    public class NewCustomerViewModel : BindableBase
    {
        public CustomerViewModel Customer { get; set; }

        public NewCustomerViewModel()
        {
            this.Customer = new CustomerViewModel();
        }

        public void CreateCustomer()
        {
            CustomersWebApiClient.CreateCustomer(new Customer
            {
                CustomerId = this.Customer.CustomerId,
                Name = this.Customer.Name,
                Phone = this.Customer.Phone,
                Address = this.Customer.Address,                
                Email = this.Customer.Email,                
                Company = this.Customer.Company,
                Title = this.Customer.Title,
                Image = this.Customer.ImagePath,                
            });
        }
    }
}
