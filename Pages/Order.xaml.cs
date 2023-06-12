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
            DataOrder();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        { 
            AddOrder addOrder = new AddOrder();
            addOrder.ShowDialog();
            DataGrid.ItemsSource = null;
            DataOrder();
        }

        public void DataOrder() 
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGrid.ItemsSource = DataDB.entities.Orders.Include(o => o.Status)
                    .Include(o => o.User)
                    .Include(o => o.DeviceType1)
                    .Include(o => o.BrandDevice1)
                    .Include(o => o.Client1)
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
                    DataOrder();
                }
            }
        }

     

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataDB.entities.SaveChanges();
            }
        }

        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                var selectedStatus = comboBox.SelectedItem as Status;
                if (selectedStatus != null)
                {
                    // Выполните нужные действия при изменении статуса
                    // Например, сохранение изменений в базе данных
                    using (DataDB.entities = new EntitiesMain())
                    {
                        DataDB.entities.SaveChanges();
                    }
                }
            }
        }

        private void CbStatus_LostFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                var selectedStatus = comboBox.SelectedItem as Status;
                if (selectedStatus != null)
                {
                    // Выполните нужные действия при изменении статуса
                    // Например, сохранение изменений в базе данных
                    using (DataDB.entities = new EntitiesMain())
                    {
                        DataDB.entities.SaveChanges();
                    }
                }
            }
        }

   

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Статус")
            {
                // Получаем выбранный объект заказа из строки редактируемой ячейки
                Orders order = e.Row.Item as Orders;

                if (order != null)
                {
                    // Получаем значение статуса из выбранного элемента комбобокса
                    ComboBox comboBox = e.EditingElement as ComboBox;
                    Status selectedStatus = comboBox.SelectedItem as Status;

                    if (selectedStatus != null)
                    {
                        // Обновляем значение статуса в объекте заказа
                        order.OrderStatus = selectedStatus.StatusId;
                        order.Status = selectedStatus;
                    }

                    // Сохраняем изменения в базе данных
                    using (DataDB.entities = new EntitiesMain())
                    {
                        DataDB.entities.Entry(order).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();
                    }
                }
            }
        }

        public void DataGridCell_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is DataGridCell cell && cell.Column.Header.ToString() == "Статус")
            {
                if (cell.Content is ComboBox comboBox)
                {
                    if (cell.DataContext is Orders order)
                    {
                        comboBox.SelectedValue = order.OrderStatus;
                    }
                }
            }
        }

    }

}
