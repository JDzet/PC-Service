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
        RegistrationProduct reg = new RegistrationProduct();
        RegistrationAdd regAdd;
        
        public WarehouseRegistration()
        {
            InitializeComponent();
            DataRegistrationProduct();
        }

        private void ButtonAddRegistration_Click(object sender, RoutedEventArgs e)
        {
            regAdd = new RegistrationAdd();
            regAdd.Owner = Window.GetWindow(this);
            regAdd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            regAdd.ShowDialog();
            DataRegistrationProduct();
        }

        public void DataRegistrationProduct()
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGrid.ItemsSource = DataDB.entities.RegistrationProduct.OrderByDescending(x=>x.RegistrationID).Include(x=>x.User)
                    .Include(x=>x.Client)
                    .Include(x=>x.WarehouseService).ToList();
                CountText.Text = "Всего записей: " + DataGrid.Items.Count.ToString();
            }
        }

        private void Btn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            reg = (sender as Button).DataContext as RegistrationProduct;

            RegistratoinInfo registratoinInfo = new RegistratoinInfo(reg);
            registratoinInfo.ShowDialog();

        }

        private void BtnDel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                reg = (sender as Button).DataContext as RegistrationProduct;
                var relatedRecords = DataDB.entities.ProductHistoryRegistration.Where(x => x.RegProduct == reg.RegistrationID).ToList();
                bool confirmed = MessageBox.Show("Удалить задачу", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                if (confirmed)
                {
                    foreach (var record in relatedRecords)
                    {
                        DataDB.entities.Entry(record).State = EntityState.Deleted;
                    }

                    DataDB.entities.Entry(reg).State = EntityState.Deleted;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Запись удалена");
                    DataRegistrationProduct();
                }
            }
        }
    }
}
