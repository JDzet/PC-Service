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
    /// Логика взаимодействия для WriteOffAddV.xaml
    /// </summary>
    public partial class WriteOffAddV : Window
    {
        public WriteOffAddV()
        {
            InitializeComponent();
            DataWrite();
        }

        public void DataWrite() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CbWarehouse.ItemsSource = DataDB.entities.WarehouseService.ToList();
            }

        }

        private void CbWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CbNameProduct.SelectedIndex = -1;
                WarehouseService selectedWarehouse = CbWarehouse.SelectedItem as WarehouseService;
                CbNameProduct.ItemsSource = DataDB.entities.ProductRemnants.Where(x => x.RemanantsWarehouse == selectedWarehouse.WarehouseID)
                     .Select(x => x.Product.ProductName)
                     .ToList();
            }
             
        }
    }
}
