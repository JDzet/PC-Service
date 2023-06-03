using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
      
        private Orders new_order = new Orders();
        private Client new_client = new Client();
        

        public AddOrder()
        {
            InitializeComponent();
            DataContext = new_order;
            Data();
           
        }

        public void Data() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CbUser.ItemsSource = DataDB.entities.Client.ToList();
                cbDevice.ItemsSource = DataDB.entities.DeviceType.ToList();
                cbBrand.ItemsSource = DataDB.entities.BrandDevice.ToList();
                cbMaster.ItemsSource = DataDB.entities.User.Where(x => x.Role.RoleId == 1).ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                var user = DataDB.entities.Client.FirstOrDefault(x => x.ClientName == CbUser.Text);
                if (user == null)
                {
                    new_client.ClientName = CbUser.Text;
                    new_client.PhoneNumber = tbPhone.Text;
                    new_client.ClientAddress = tbAdress.Text;
                    new_client.ClientEmail = tbEmail.Text;
                    DataDB.entities.Client.Add(new_client);
                    DataDB.entities.SaveChanges();
                }



                var usernow = DataDB.entities.Client.FirstOrDefault(x => x.ClientName == CbUser.Text);

                if (new_order.OrderID == 0)
                {
                    new_order.Client = usernow.ClientId;
                    new_order.Status = DataDB.entities.Status.First();
                    var id = DataDB.entities.User.FirstOrDefault(x => x.UserId == 2);
                    new_order.UserReceipt = id.UserId;
                    new_order.ReceiptTime = dpReceiptTime.SelectedDate.Value;
                    new_order.Deadline = dpDeadLine.SelectedDate.Value;
                    DataDB.entities.Orders.Add(new_order);
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Заказ добавлен");
                    this.Close();
                }
            }
           


        }

        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Client client)
                {
                    tbPhone.Text = client.PhoneNumber;
                    tbEmail.Text = client.ClientEmail;
                    tbAdress.Text = client.ClientAddress;
                }
                else    // Обработка случая, когда выбранный элемент равен null
                {
                
                    tbPhone.Text = string.Empty;
                    tbEmail.Text = string.Empty;
                    tbAdress.Text = string.Empty;
                }
            }

        }
    }
}
