using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Entities.Filters;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.ui
{
    public partial class OrderAddWindow : Window
    {
        private readonly IStoreService _storeService;
        public int? CustomerId { get; private set; }
        public DateTime? Date { get; private set; }
        
        public ObservableCollection<OrderItemInput> OrderItems { get; } = new();

        public OrderAddWindow(IStoreService storeService)
        {
            InitializeComponent();
            DataContext = this;
            _storeService = storeService;
            OrderItems.Add(new OrderItemInput());
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            OrderItems.Add(new OrderItemInput());
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (GridOrderItems.SelectedItem is OrderItemInput row)
                OrderItems.Remove(row);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TbCustomerId.Text?.Trim(), out var cid))
            {
                MessageBox.Show("Please provide a valid CustomerId (integer).");
                return;
            }
            
            if (_storeService.GetList<Customer>(new CustomerFilter { Id = cid }).Count == 0)
            {
                MessageBox.Show($"The Customer with Id={cid} does not exist in the database.");
                return;
            }

            
            var validItems = OrderItems
                .Where(i => i.ProductId.HasValue && i.ProductId.Value > 0
                            && i.Quantity.HasValue && i.Quantity.Value > 0)
                .ToList();
            
            if (validItems.Count == 0 || validItems.Count != OrderItems.Count)
            {
                MessageBox.Show("All products must have the correct Id and positive quantity.");
                return;
            }

            foreach (var item in validItems)
            {
                var products = _storeService.GetList<Product>(new ProductFilter { Id = item.ProductId!.Value });
                if (products.Count == 0)
                {
                    MessageBox.Show($"The product with Id = {item.ProductId!.Value} does not exist in the database.");
                    return;
                }

                if (products.First().Quantity < item.Quantity)
                {
                    MessageBox.Show($"The product with Id = {item.ProductId!.Value} is only available in this quantity: {products.First().Quantity}.");
                    return;
                }
            }
            
            CustomerId = cid;
            Date = DateTime.Now;

            DialogResult = true;
            Close();
        }
    }
    
    public class OrderItemInput
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
