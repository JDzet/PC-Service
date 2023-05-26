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

namespace PC_Service.Pages.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для WarehouseRemains.xaml
    /// </summary>
    public partial class WarehouseRemains : Page
    {
        EntitiesMain entities = new EntitiesMain();
        public WarehouseRemains()
        {
            InitializeComponent();
            DataGrid.ItemsSource = entities.ProductRemnants.ToList();
        }


    }
}
