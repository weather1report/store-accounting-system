using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using store_accounting_system.core.Interfaces;
using store_accounting_system.core.Services;

namespace store_accounting_system.ui
{
    public partial class MainWindow : Window
    {
        private readonly IStoreService _storeService;
        
        public MainWindow(IStoreService storeService)
        {
            InitializeComponent();
            _storeService = storeService;
    
            
            // TODO: здесь можно загрузить первоначальные данные во все таблицы
            // RefreshProductsGrid();
            // RefreshCustomersGrid();
            // RefreshOrdersGrid();
            // RefreshOrderItemsGrid();
            // RefreshSuppliesGrid();
        }

        // ===================== Products =====================
        private void BtnSearchProducts_Click(object sender, RoutedEventArgs e)
        {
            var idText = TbProductId.Text?.Trim();
            var name = TbProductName.Text?.Trim();
            var priceText = TbProductPrice.Text?.Trim();
            var quantityText = TbProductQuantity.Text?.Trim();

            // TODO: вызвать Core/Data с фильтром
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                // TODO: вызвать Core/Data для создания:
                // var dto = new ProductDto { Name = dlg.ProductName, Price = dlg.ProductPrice, Quantity = dlg.ProductQuantity };
                // await _productService.CreateAsync(dto);
                // RefreshProductsGrid();
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                // TODO: await _productService.DeleteAsync(id);
                // RefreshProductsGrid();
            }
        }

        private void RefreshProductsGrid()
        {
            // TODO: загрузить все продукты
        }

        // ===================== Customers =====================
        private void BtnSearchCustomers_Click(object sender, RoutedEventArgs e)
        {
            var idText = TbCustomerId.Text?.Trim();
            var name = TbCustomerName.Text?.Trim();
            var phone = TbCustomerPhoneNumber.Text?.Trim();
            var dateFrom = DpCustomerRegisterDateFrom.SelectedDate;
            var dateTo = DpCustomerRegisterDateTo.SelectedDate;

            // TODO: вызвать Core/Data с фильтром по Id, Name, Phone, DateFrom, DateTo
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CustomerAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                // TODO:
                // var dto = new CustomerDto { Name = dlg.CustomerName, PhoneNumber = dlg.CustomerPhoneNumber, RegisterDate = dlg.RegisterDate };
                // await _customerService.CreateAsync(dto);
                // RefreshCustomersGrid();
            }
        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CustomerDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                // TODO: await _customerService.DeleteAsync(id);
                // RefreshCustomersGrid();
            }
        }

        private void RefreshCustomersGrid()
        {
            // TODO: загрузить всех клиентов
        }

        // ===================== Orders =====================
        private void BtnSearchOrders_Click(object sender, RoutedEventArgs e)
        {
            var idText = TbOrderId.Text?.Trim();
            var customerIdText = TbOrderCustomerId.Text?.Trim();
            var totalAmountText = TbOrderTotalAmount.Text?.Trim();
            var dateFrom = DpOrderDateFrom.SelectedDate;
            var dateTo = DpOrderDateTo.SelectedDate;

            // TODO: вызвать Core/Data с фильтром по Id, CustomerId, TotalAmount, DateFrom, DateTo
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                // TODO:
                // var dto = new OrderDto { CustomerId = dlg.CustomerId, TotalAmount = dlg.TotalAmount, Date = dlg.Date };
                // await _orderService.CreateAsync(dto);
                // RefreshOrdersGrid();
            }
        }

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                // TODO: await _orderService.DeleteAsync(id);
                // RefreshOrdersGrid();
            }
        }

        private void RefreshOrdersGrid()
        {
            // TODO: загрузить все заказы
        }

        // ===================== OrderItems =====================
        private void BtnSearchOrderItems_Click(object sender, RoutedEventArgs e)
        {
            var idText = TbOrderItemId.Text?.Trim();
            var orderIdText = TbOrderItemOrderId.Text?.Trim();
            var countText = TbOrderItemCount.Text?.Trim();

            // TODO: вызвать Core/Data с фильтром по Id, OrderId, Count
        }

        private void BtnAddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderItemAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                // TODO:
                // var dto = new OrderItemDto { OrderId = dlg.OrderId, Count = dlg.Count };
                // await _orderItemService.CreateAsync(dto);
                // RefreshOrderItemsGrid();
            }
        }

        private void BtnDeleteOrderItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderItemDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                // TODO: await _orderItemService.DeleteAsync(id);
                // RefreshOrderItemsGrid();
            }
        }

        private void RefreshOrderItemsGrid()
        {
            // TODO: загрузить все позиции заказов
        }

        // ===================== Supplies =====================
        private void BtnSearchSupplies_Click(object sender, RoutedEventArgs e)
        {
            var idText = TbSupplyId.Text?.Trim();
            var dateFrom = DpSupplyDateFrom.SelectedDate;
            var dateTo = DpSupplyDateTo.SelectedDate;
            var productIdText = TbSupplyProductId.Text?.Trim();
            var quantityText = TbSupplyQuantity.Text?.Trim();

            // TODO: вызвать Core/Data с фильтром по Id, DateFrom, DateTo, ProductId, Quantity
        }

        private void BtnAddSupply_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SupplyAddWindow { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                // TODO:
                // var dto = new SupplyDto { Date = dlg.Date, ProductId = dlg.ProductId, Quantity = dlg.Quantity };
                // await _supplyService.CreateAsync(dto);
                // RefreshSuppliesGrid();
            }
        }

        private void BtnDeleteSupply_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SupplyDeleteWindow { Owner = this };
            if (dlg.ShowDialog() == true && dlg.Id.HasValue)
            {
                var id = dlg.Id.Value;
                // TODO: await _supplyService.DeleteAsync(id);
                // RefreshSuppliesGrid();
            }
        }

        private void RefreshSuppliesGrid()
        {
            // TODO: загрузить все поставки
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
    }
}
