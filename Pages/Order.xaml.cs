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
        DemoMainEntities DemoMainEntities = new DemoMainEntities();
        public Order()
        {
            InitializeComponent();
            Grid.ItemsSource = DemoMainEntities.Product.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void Page_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}
