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
using System.Windows.Shapes;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private readonly Request _request;
        private Client _client;

        public AddClient(Client client, Request req)
        {
            InitializeComponent();
            _client = client ?? new Client();
            _request = req;
            DataContext = _client;
        }

        private void DataBaseClientAdd_Click(object sender, RoutedEventArgs e)
        {
            
             _client = (Client)this.DataContext;

            _request.WorkClient(_client);
            this.Close();
        }
    }
}
