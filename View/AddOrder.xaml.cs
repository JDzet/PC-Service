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
        EntitiesMain entities = new EntitiesMain();
        private Orders new_order = new Orders();
        private Client new_client = new Client();
        Order page = new Order();

        public AddOrder()
        {
            InitializeComponent();
            DataContext = new_order;
            cbDevice.ItemsSource = entities.DeviceType.ToList();
            cbBrand.ItemsSource = entities.BrandDevice.ToList();
            cbMaster.ItemsSource = entities.User.Where(x => x.Role.RoleId == 1).ToList();
           
        }

        private void ComboBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = entities.Client.FirstOrDefault(x => x.ClientName == tbName.Text);
            if (user == null)
            {
                new_client.ClientName = tbName.Text;
                new_client.PhoneNumber = tbPhone.Text;
                new_client.ClientAddress = tbAdress.Text;
                entities.Client.Add(new_client);
                entities.SaveChanges();
            }



            var usernow = entities.Client.FirstOrDefault(x => x.ClientName == tbName.Text);

            if (new_order.OrderID == 0) 
            {
                new_order.Client = usernow.ClientId;
                new_order.Status = entities.Status.First();
                var id = entities.User.FirstOrDefault(x => x.UserId == 2);
                new_order.UserReceipt = id.UserId;
                new_order.ReceiptTime = dpReceiptTime.SelectedDate.Value;
                new_order.Deadline = dpDeadLine.SelectedDate.Value;
                entities.Orders.Add(new_order);
                entities.SaveChanges();
                MessageBox.Show("Заказ добавлен");
                this.Close();
            }


        }
    }
}
