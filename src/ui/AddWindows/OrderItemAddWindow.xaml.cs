using System.Windows;

namespace store_accounting_system.ui
{
    public partial class OrderItemAddWindow : Window
    {
        public int? OrderId { get; private set; }
        public int? Count { get; private set; }

        public OrderItemAddWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbOrderId.Text?.Trim(), out var oid)) OrderId = oid;
            if (int.TryParse(TbCount.Text?.Trim(), out var c)) Count = c;

            if (OrderId == null || Count == null)
            {
                MessageBox.Show("OrderId and Count are required.");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}