using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        
        AddClient WindowClient;
       

        public ClientPage()
        {
            InitializeComponent();
            DataBase();
        }

        private void ButAddClient_Click(object sender, RoutedEventArgs e)
        {
            WindowClient = new AddClient(null);
            WindowClient.Owner = Window.GetWindow(this);
            WindowClient.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowClient.ShowDialog();
            DataGridClient.ItemsSource = null;
            DataBase();
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            WindowClient = new AddClient((sender as Button).DataContext as Client); 
            WindowClient.ShowDialog();
            DataGridClient.ItemsSource = null;
            DataBase();

        }

        public void DataBase() // заполнения данными из бд
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGridClient.ItemsSource = DataDB.entities.Client.ToList();
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = (sender as Button).DataContext as Client;
                using (DataDB.entities = new EntitiesMain())
                {
                    bool confirmed = MessageBox.Show("Удалить клиента", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                    if (confirmed)
                    {

                        DataDB.entities.Entry(client).State = EntityState.Deleted;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Запись удалена");
                        DataBase();
                    }
                }
            }
            catch(DbUpdateException) 
            {
                MessageBox.Show("Клиент учавствует в заказе.\nЗакройте и удалите все связанные с пользователем заказы, перед удалением.", "Внимание");
            }
            
        }
    }
}
