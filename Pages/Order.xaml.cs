using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
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
        public Order()
        {
            InitializeComponent();
            Data();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        { 
            AddOrder addOrder = new AddOrder();
            addOrder.Owner = Window.GetWindow(this); 
            addOrder.WindowStartupLocation = WindowStartupLocation.CenterOwner; 
            addOrder.ShowDialog();
            Data();
        }

        public void Data() 
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGrid.ItemsSource = DataDB.entities.Orders.Include(o => o.Status)
                    .Include(o => o.User)
                    .Include(o => o.DeviceType1)
                    .Include(o => o.BrandDevice1)
                    .Include(o => o.Client1)
                    .Include(o => o.Status)
                    .ToList();
                 CbStatus.ItemsSource = DataDB.entities.Status.ToList();
             


            }
                

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
            using (DataDB.entities = new EntitiesMain())
            {
                Orders orders = (sender as Button).DataContext as Orders;
                if (orders != null && orders.FileData != null)
                {
                    string tempFilePath = System.IO.Path.GetTempFileName();
                    File.WriteAllBytes(tempFilePath, orders.FileData);

                    Process.Start(tempFilePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден");
                }
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            Orders orders = (sender as Button).DataContext as Orders;
            bool confirmed = MessageBox.Show("Удалить данное заказ", "Внимамние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            using (DataDB.entities = new EntitiesMain())
            {
                if (confirmed)
                {

                    DataDB.entities.Entry(orders).State = EntityState.Deleted;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Data();
                }
            }
        }

        static Orders orders;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CbStatus.IsEnabled = true;
                orders = DataGrid.SelectedItem as Orders;

                if(orders != null)
                    CbStatus.SelectedItem = orders.Status;
            }
            catch 
            {

            }
           
        }

        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                try 
                {
                    Status status = CbStatus.SelectedItem as Status;
                    if (status != null) 
                    {
                        orders.OrderStatus = status.StatusId;
                        orders.Status = status;

                        DataDB.entities.Entry(orders).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();

                        List<Orders> ordersList = DataDB.entities.Orders.Include(o => o.Status)
                            .Include(o => o.User)
                            .Include(o => o.DeviceType1)
                            .Include(o => o.BrandDevice1)
                            .Include(o => o.Client1)
                            .Include(o => o.Status)
                            .ToList();
                        DataGrid.ItemsSource = ordersList;
                    }
                   
                }
                catch
                {

                }
            }

        }
    }

}
