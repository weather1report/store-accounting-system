using System;
using System.Windows;

namespace store_accounting_system.ui
{
    public partial class CustomerAddWindow : Window
    {
        public string CustomerName { get; private set; } = "";
        public string CustomerPhoneNumber { get; private set; } = "";
        public DateTime? RegisterDate { get; private set; }

        public CustomerAddWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerName = TbName.Text?.Trim() ?? "";
            CustomerPhoneNumber = TbPhone.Text?.Trim() ?? "";
            RegisterDate = DpRegisterDate.SelectedDate;

            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}