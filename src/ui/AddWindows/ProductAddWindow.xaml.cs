using System.Globalization;
using System.Windows;

namespace store_accounting_system.ui
{
    public partial class ProductAddWindow : Window
    {
        public string ProductName { get; private set; } = "";
        public decimal? ProductPrice { get; private set; }

        public ProductAddWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductName = TbName.Text?.Trim() ?? "";
            if (decimal.TryParse(TbPrice.Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                ProductPrice = price;

            if (ProductPrice <= 0)
            {
                MessageBox.Show("Price must be greater than zero.");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}