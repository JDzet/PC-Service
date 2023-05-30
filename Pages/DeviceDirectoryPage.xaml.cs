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
    /// Логика взаимодействия для DeviceDirectoryPage.xaml
    /// </summary>
    public partial class DeviceDirectoryPage : Page
    {
        public DeviceDirectoryPage()
        {
            InitializeComponent();
            DataDevice();
        }

        public void DataDevice() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGridDevice.ItemsSource = DataDB.entities.DeviceType.ToList();
            }
        }

        private void DataGridDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DeviceType type = new DeviceType();
                type = DataGridDevice.SelectedItem as DeviceType;
                DataGridBrand.ItemsSource = DataDB.entities.BrandDevice.Where(x => x.Type == type.DeviceTypeId).ToList();
            }
               
        }

        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Menu("Устройство");
        }
        public void Menu(string str) 
        {
            TbHeader.Text = str;
            FormContainer.Visibility = Visibility.Visible;


            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private void BtModel_Click(object sender, RoutedEventArgs e)
        {
            
            Menu("Модель");
        }
    }
}
