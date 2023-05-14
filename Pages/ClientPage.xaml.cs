using PC_Service.View;
using System;
using System.Collections.Generic;
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
        Request request = new Request();
        AddClient WindowClient;

        public ClientPage()
        {
            InitializeComponent();
            DataGridClient.ItemsSource = request.GridClient();
            
        }

        private void ButAddClient_Click(object sender, RoutedEventArgs e)
        {
            WindowClient = new AddClient(null, request);
            WindowClient.ShowDialog();
            DataGridClient.ItemsSource = null;
            DataGridClient.ItemsSource = request.GridClient();


        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            WindowClient = new AddClient((sender as Button).DataContext as Client, request);
            WindowClient.ShowDialog();
            DataGridClient.ItemsSource = null;
            DataGridClient.ItemsSource = request.GridClient();

        }
    }
}
