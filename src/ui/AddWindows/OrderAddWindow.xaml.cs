using System;
using System.Globalization;
using System.Windows;

namespace store_accounting_system.ui
{
    public partial class OrderAddWindow : Window
    {
        public int? CustomerId { get; private set; }
        public decimal? TotalAmount { get; private set; }
        public DateTime? Date { get; private set; }

        public OrderAddWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbCustomerId.Text?.Trim(), out var cid)) CustomerId = cid;
            if (decimal.TryParse(TbTotalAmount.Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var amt)) TotalAmount = amt;
            Date = DpDate.SelectedDate;

            if (CustomerId == null || Date == null)
            {
                MessageBox.Show("CustomerId and Date are required.");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}