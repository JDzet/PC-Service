using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting.Contexts;

namespace PC_Service.Pages.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для WarehouseWriteOff.xaml
    /// </summary>
    public partial class WarehouseWriteOff : Page
    {
        public WarehouseWriteOff()
        {
            InitializeComponent();
            DataWriteOff();
        }

        public void DataWriteOff() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.ProductWriteOff.OrderByDescending(x=>x.WriteOffID).Include(x=>x.User).Include(x=>x.WarehouseService).ToList();
                CountText.Text = "Записей: " + DataGrid.Items.Count.ToString();
            }
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            WriteOffAddV offAddV = new WriteOffAddV();
            offAddV.ShowDialog();
            DataWriteOff();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            ProductWriteOff writeOff = new ProductWriteOff();
            writeOff = (sender as Button).DataContext as ProductWriteOff;

            bool confirmed = MessageBox.Show("Удалить данное списание", "Внимамние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            using (DataDB.entities = new EntitiesMain()) 
            {
                var product = DataDB.entities.ProductWriteOffHistory.Where(x => x.ProductWriteOff == writeOff.WriteOffID).ToList();
                if (confirmed)
                {
                    foreach (var record in product) 
                    {
                        DataDB.entities.Entry(record).State = EntityState.Deleted;
                    }

                    DataDB.entities.Entry(writeOff).State = EntityState.Deleted;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DataWriteOff();
                }
            }
                
            
        }

        private void Btn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ProductWriteOff writeOff = new ProductWriteOff();
            writeOff = (sender as Button).DataContext as ProductWriteOff;
            WriteOffInfo info = new WriteOffInfo(writeOff);
            info.ShowDialog();
        }
    }
}
