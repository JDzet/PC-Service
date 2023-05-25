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
    /// Логика взаимодействия для WarehouseProduct.xaml
    /// </summary>
    public partial class WarehouseProduct : Page
    {
        public WarehouseProduct()
        {
            InitializeComponent();
            DataProduct();
        }

        public void DataProduct() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.Product.ToList();
            }
        }
    }
}
