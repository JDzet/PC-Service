using ControlzEx.Standard;
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
using System.Windows.Media.Animation;
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

        WarehouseService warehouse;
        ApiYandex api = new ApiYandex();
        public WarehouseLokal()
        {
            InitializeComponent();
            DataWarehouseLokal();
        }

        public void DataWarehouseLokal() // подгрузка данных 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.WarehouseService.ToList();
                Count.Text = "Количество складов: " + DataGrid.Items.Count.ToString();
            }
        }

        private void AddWarehouseClickClick(object sender, RoutedEventArgs e)
        {
            warehouse = new WarehouseService();
            editWarehouse();
        }

        public void Menu() // работа над меню редактирования 
        {
            FormContainer.Visibility = Visibility.Visible;

           
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 250; // Ширина формы 
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private async void TbAddress_TextChanged(object sender, TextChangedEventArgs e) 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                string searchText = TbAddress.Text;
                List<string> filteredAdress = await api.GetAutocompleteAddresses(searchText);
                TbAddress.ItemsSource = filteredAdress;
                TbAddress.IsDropDownOpen = true;
            }
                
        } // работа с api

        private void BtAdd_Click(object sender, RoutedEventArgs e) // действие на кнопку добавления 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                try 
                {
                    if (warehouse.WarehouseID != 0)
                    {
                        DataDB.entities.Entry(warehouse).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                    }
                    else
                    {
                        DataDB.entities.WarehouseService.Add(warehouse);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                FormContainer.Visibility = Visibility.Collapsed;
            }
            DataWarehouseLokal();
        }

        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) // действие на кнопкку редактирования
        {
            warehouse = (sender as Button).DataContext as WarehouseService;
            editWarehouse();

        }

        public void editWarehouse() // процес создания/редактирования склада
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
               
               WarehouseService _editwarehouse = warehouse ?? new WarehouseService();

                DataContext = _editwarehouse;
                Menu();

            }
                
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                warehouse = (sender as Button).DataContext as WarehouseService;
                bool confirmed = MessageBox.Show("Вы точно хотите удалить этот склад", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                if (confirmed)
                {
                    try
                    {
                        DataDB.entities.Entry(warehouse).State = EntityState.Deleted;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Склад удален");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    DataWarehouseLokal();
                }
            }
               

        }
    }
}
