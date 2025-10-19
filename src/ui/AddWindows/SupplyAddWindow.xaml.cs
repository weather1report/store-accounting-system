using System;
using System.Windows;

namespace store_accounting_system.ui
{
    public partial class SupplyAddWindow : Window
    {
        public DateTime? Date { get; private set; }
        public int? ProductId { get; private set; }
        public int? Quantity { get; private set; }

        public SupplyAddWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Date = DpDate.SelectedDate;
            if (int.TryParse(TbProductId.Text?.Trim(), out var pid)) ProductId = pid;
            if (int.TryParse(TbQuantity.Text?.Trim(), out var qty)) Quantity = qty;

            if (Date == null || ProductId == null || Quantity == null)
            {
                MessageBox.Show("Date, ProductId and Quantity are required.");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}