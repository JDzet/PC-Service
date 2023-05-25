using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;
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
    /// Логика взаимодействия для WarehouseRegistration.xaml
    /// </summary>
    public partial class WarehouseRegistration : Page
    {
        
        RegistrationAdd regAdd;
        public WarehouseRegistration()
        {
            InitializeComponent();
            DaRegistrationProduct();
        }

        private void ButtonAddRegistration_Click(object sender, RoutedEventArgs e)
        {
            regAdd = new RegistrationAdd();
            regAdd.ShowDialog();
        }

        public void DaRegistrationProduct()
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGrid.ItemsSource = DataDB.entities.RegistrationProduct.Include(x=>x.User)
                    .Include(x=>x.Client)
                    .Include(x=>x.WarehouseService).ToList();
            }
        }
    }
}
