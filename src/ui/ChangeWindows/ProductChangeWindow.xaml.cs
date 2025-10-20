using System;
using System.Globalization;
using System.Windows;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Entities.Filters;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.ui
{
    public partial class ProductChangeWindow : Window
    {
        
        public int? ProductId { get; private set; }

        public string ProductName { get; private set; } = "";
        public decimal? Price { get; private set; }
        
        private readonly IStoreService _storeService;

        public ProductChangeWindow(IStoreService storeService)
        {
            InitializeComponent();
            _storeService = storeService;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductName = TbName.Text?.Trim() ?? "";
            if (int.TryParse(TbProductId.Text?.Trim(), out var pid)) ProductId = pid;
            if (decimal.TryParse(TbPrice.Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                Price = price;

            if (ProductId == null || ((ProductName == "") && (Price == null)))
            {
                MessageBox.Show("ProductId and New Name, Price are required.");
                return;
            }

            if (_storeService.GetList<Product>(new ProductFilter { Id = ProductId }).Count == 0)
            {
                MessageBox.Show($"Product with Id={ProductId} does not exist.");
                return;
            }

            if (Price <= 0)
            {
                MessageBox.Show("Price must be greater than zero.");
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}