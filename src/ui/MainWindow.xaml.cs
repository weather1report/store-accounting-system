using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Entities.Filters;
using store_accounting_system.core.Interfaces;
using store_accounting_system.core.Services;
using store_accounting_system.ui.ViewModels;

namespace store_accounting_system.ui
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;
        
        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            _vm = (MainViewModel)DataContext;
            InitializeComponent();
            

        }

        // ===================== Products =====================
        private void BtnSearchProducts_Click(object sender, RoutedEventArgs e)
        {
            var id = ParseInt(TbProductId.Text?.Trim());
            var name = TbProductName.Text?.Trim();
            var maxPrice = ParseDecimal(TbProductMaxPrice.Text?.Trim());
            var minPrice = ParseDecimal(TbProductMinPrice.Text?.Trim());
            var minQuantity = ParseInt(TbProductMinQuantity.Text?.Trim());
            var maxQuantity = ParseInt(TbProductMaxQuantity.Text?.Trim());

            RefreshProductsGrid(new ProductFilter{Id = id, Name = name, MaxPrice = maxPrice, MinPrice = minPrice, MinQuantity = minQuantity, MaxQuantity = maxQuantity});
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                _vm.StoreService.Add(new Product{Id = 0, Name = dlg.ProductName, Price = dlg.ProductPrice ?? 0, Quantity = 0});
                RefreshProductsGrid();
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                _vm.StoreService.DeleteById<Product>(id);
                RefreshProductsGrid();
            }
        }
        
        private void BtnChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductChangeWindow(_vm.StoreService){ Owner = this };
            if (dlg.ShowDialog() == true)
            {
                var id = dlg.ProductId!.Value;
                var product = _vm.StoreService.GetList<Product>(new ProductFilter { Id = id }).First();
                
                if (dlg.ProductName != "") product.Name = dlg.ProductName;
                if (dlg.Price.HasValue) product.Price = dlg.Price.Value;

                _vm.StoreService.Update(product);
                RefreshProductsGrid();
            }
        }

        private void RefreshProductsGrid(IFilter<Product>? filter = null)
        {
            _vm.Products.Clear();
            foreach (var product in _vm.StoreService.GetList<Product>(filter))
                _vm.Products.Add(product);
        }

        // ===================== Customers =====================
        private void BtnSearchCustomers_Click(object sender, RoutedEventArgs e)
        {
            var id = ParseInt(TbCustomerId.Text?.Trim());
            var name = TbCustomerName.Text?.Trim();
            var phone = TbCustomerPhoneNumber.Text?.Trim();
            var dateFrom = DpCustomerRegisterDateFrom.SelectedDate;
            var dateTo = DpCustomerRegisterDateTo.SelectedDate;

            RefreshCustomersGrid(new CustomerFilter{Id = id, Name = name, Phone = phone, RegisteredAfter = dateFrom, RegisteredBefore = dateTo});
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CustomerAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                _vm.StoreService.Add(new Customer{Id = 0, Name = dlg.CustomerName, PhoneNumber = dlg.CustomerPhoneNumber, RegisterDate = DateTime.Now});
                RefreshCustomersGrid();
            }
        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CustomerDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                _vm.StoreService.DeleteById<Customer>(id);
                RefreshCustomersGrid();
            }
        }

        private void RefreshCustomersGrid(IFilter<Customer>? filter = null)
        {
            _vm.Customers.Clear();
            foreach (var customer in _vm.StoreService.GetList<Customer>(filter))
                _vm.Customers.Add(customer);
        }

        // ===================== Orders =====================
        private void BtnSearchOrders_Click(object sender, RoutedEventArgs e)
        {
            var id = ParseInt(TbOrderId.Text?.Trim());
            var customerId = ParseInt(TbOrderCustomerId.Text?.Trim());
            var minTotalAmount = ParseDecimal(TbOrderMinTotalAmount.Text?.Trim());
            var maxTotalAmount = ParseDecimal(TbOrderMaxTotalAmount.Text?.Trim());
            var dateFrom = DpOrderDateFrom.SelectedDate;
            var dateTo = DpOrderDateTo.SelectedDate;

            RefreshOrdersGrid(new OrderFilter
            {
                Id = id,
                CustomerId = customerId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                MinTotal = minTotalAmount,
                MaxTotal = maxTotalAmount,
            });
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e) 
        {
            var dlg = new OrderAddWindow(_vm.StoreService) { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                int customerId = dlg.CustomerId!.Value;
                DateTime date = dlg.Date!.Value;

                var order = new Order { CustomerId = customerId, Date = date, Id = 0, OrderItems = new(), TotalAmount = 0};
                foreach (var orderItem in dlg.OrderItems)
                {
                    order.OrderItems.Add(new OrderItem{Id = 0, ProductId = orderItem.ProductId ?? 0, Count = orderItem.Quantity ?? 0, Date = date});
                }
                
                _vm.StoreService.Add(order);
                RefreshOrdersGrid();
                RefreshCustomersGrid();
            }
        }

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                _vm.StoreService.DeleteById<Order>(id);
                RefreshOrdersGrid();
            }
        }

        private void RefreshOrdersGrid(IFilter<Order>? filter = null)
        {
            _vm.Orders.Clear();
            foreach (var order in _vm.StoreService.GetList<Order>(filter))
                _vm.Orders.Add(order);
        }

        // ===================== OrderItems =====================
        private void BtnSearchOrderItems_Click(object sender, RoutedEventArgs e)
        {
            var id = ParseInt(TbOrderItemId.Text?.Trim());
            var orderId = ParseInt(TbOrderItemOrderId.Text?.Trim());
            var productId = ParseInt(TbOrderItemProductId.Text?.Trim());
            var dateFrom = DpOrderItemDateFrom.SelectedDate;
            var dateTo = DpOrderDateTo.SelectedDate;

            RefreshOrderItemsGrid(new OrderItemFilter{Id = id, OrderId = orderId, ProductId = productId,  DateFrom = dateFrom, DateTo = dateTo});
        }

        private void RefreshOrderItemsGrid(IFilter<OrderItem>? filter = null)
        {
            _vm.OrderItems.Clear();
            foreach (var orderItem in _vm.StoreService.GetList<OrderItem>(filter))
                _vm.OrderItems.Add(orderItem);
        }

        // ===================== Supplies =====================
        private void BtnSearchSupplies_Click(object sender, RoutedEventArgs e)
        {
            var id = ParseInt(TbSupplyId.Text?.Trim());
            var dateFrom = DpSupplyDateFrom.SelectedDate;
            var dateTo = DpSupplyDateTo.SelectedDate;
            var productId = ParseInt(TbSupplyProductId.Text?.Trim());

            RefreshSuppliesGrid(new SupplyFilter{Id = id, DateFrom = dateFrom, DateTo = dateTo, ProductId = productId});
        }

        private void BtnAddSupply_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SupplyAddWindow(_vm.StoreService) { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                _vm.StoreService.Add(new Supply{Date = dlg.Date ?? DateTime.Now, Id = 0, ProductId = dlg.ProductId ?? 0, Quantity = dlg.Quantity ?? 0});
                RefreshSuppliesGrid();
            }
        }

        private void BtnDeleteSupply_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SupplyDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                _vm.StoreService.DeleteById<Supply>(id);
                RefreshSuppliesGrid();
            }
        }

        private void RefreshSuppliesGrid(IFilter<Supply>? filter = null)
        {
            _vm.Supplies.Clear();
            foreach (var supply in _vm.StoreService.GetList<Supply>(filter))
                _vm.Supplies.Add(supply);
        }

        // ===================== Вспомогательные =====================
        private int? ParseInt(string? text)
        {
            if (int.TryParse(text, out var val)) return val;
            return null;
        }

        private decimal? ParseDecimal(string? text)
        {
            if (decimal.TryParse(text, out var val)) return val;
            return null;
        }
        
        private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl) return;
            
            if (MainTabs.SelectedItem is TabItem tab)
            {
                switch (tab.Header)
                {
                    case "Products":
                        RefreshProductsGrid();
                        break;
                    case "Customers":
                        RefreshCustomersGrid();
                        break;
                    case "Orders":
                        RefreshOrdersGrid();
                        break;
                    case "OrderItems":
                        RefreshOrderItemsGrid();
                        break;
                    case "Supplies":
                        RefreshSuppliesGrid();
                        break;
                }
            }
        }
    }
}
