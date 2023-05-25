using ControlzEx.Standard;
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
        ApiYandex api = new ApiYandex();
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

        private void AddWarehouseClickClick(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Visible;

            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 300; // Ширина формы (можете настроить под свои нужды)
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
                
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                



            }

        }

        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
        }
    }
}
