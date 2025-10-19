using System.Windows;

namespace store_accounting_system.ui
{
    public partial class OrderItemDeleteWindow : Window
    {
        public int? Id { get; private set; }

        public OrderItemDeleteWindow()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbId.Text?.Trim(), out var id))
            {
                Id = id;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Enter a valid numeric Id.");
            }
        }
    }
}