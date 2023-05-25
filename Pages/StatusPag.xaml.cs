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
    /// Логика взаимодействия для StatusPag.xaml
    /// </summary>
    public partial class StatusPag : Page
    {
        public StatusPag()
        {
            InitializeComponent();
            DataStatus();
        }

        public void DataStatus() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.Status.ToList();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Visible;

            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }


        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;

        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                Status status = new Status();
                status.StatusName = TbAddStatus.Text;
                DataDB.entities.Status.Add(status);
                DataDB.entities.SaveChanges();
                MessageBox.Show("Данные сохранены");
                WatchtCases_Click(sender, e);
            }

        }
    }
}
