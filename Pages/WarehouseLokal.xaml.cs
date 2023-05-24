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
    /// Логика взаимодействия для WarehouseLokal.xaml
    /// </summary>
    public partial class WarehouseLokal : Page
    {
        public WarehouseLokal()
        {
            InitializeComponent();
            DataWarehouseLokal();
        }

        public void DataWarehouseLokal() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.Warehouse.ToList();
            }
        }
    }
}
