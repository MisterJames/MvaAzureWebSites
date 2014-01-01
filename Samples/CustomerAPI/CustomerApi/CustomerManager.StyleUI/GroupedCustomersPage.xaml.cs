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

namespace CustomerManager.StyleUI
{
    using System;
    using System.Collections.Generic;
    using CustomerManager.StyleUI.Common;
    using CustomerManager.StyleUI.DataModel;
    using CustomerManager.StyleUI.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class GroupedCustomersPage : LayoutAwarePage
    {
        private GroupedCustomersViewModel ViewModel { get; set; }

        public GroupedCustomersPage()
        {            
            this.InitializeComponent();
            this.ViewModel = new GroupedCustomersViewModel();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {            
            this.DataContext = this.ViewModel;
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void CustomerItem_Click(object sender, ItemClickEventArgs e)
        {
            var customerId = ((CustomerViewModel)e.ClickedItem).CustomerId;            
            
            this.Frame.Navigate(typeof(CustomerDetailPage), customerId);
        }

        private void NewCustomer_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewCustomerPage));
        }
    }
}
