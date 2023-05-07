using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PC_Service
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        EntitiesMain Entities = new EntitiesMain();
       
        public Order()
        {
            InitializeComponent();
            DataGrid.ItemsSource = Entities.Orders.ToList();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        { 
            AddOrder addOrder = new AddOrder();
            addOrder.ShowDialog();
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = Entities.Orders.ToList();
        }
    }

}
