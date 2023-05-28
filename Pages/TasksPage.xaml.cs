using PC_Service.Pages.TasksPages;
using PC_Service.Pages.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
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
using System.Windows.Media.Animation;

namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();
            DataTask();
        }

        public void DataTask()
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGridTaskAtWork.ItemsSource = DataDB.entities.Task.Include(x => x.User).Where(x=>x.TaskStatus == false).ToList();
                DataGridWorkCompletedk.ItemsSource = DataDB.entities.Task.Include(x => x.User).Where(x => x.TaskStatus == true).ToList();
            }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
               
                

                bool confirmed = MessageBox.Show("Вы уверены, что хотите изменить статус задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                if (confirmed)
                {
                    CheckBox checkBox = (CheckBox)e.OriginalSource;
                    Task task = checkBox.DataContext as Task;
                    task.TaskStatus = true;
                    DataDB.entities.Entry(task).State = EntityState.Modified;
                    DataDB.entities.SaveChanges();
                    DataTask();

                }
                else
                {
                    CheckBox check = (CheckBox)e.OriginalSource;
                    check.IsChecked = false;
                }
            }
        }

        private void CheckBox_CheckedDel(object sender, RoutedEventArgs e) 
        {
            using (DataDB.entities = new EntitiesMain())
            {



                bool confirmed = MessageBox.Show("Вы уверены, что хотите изменить статус задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                if (confirmed)
                {
                    CheckBox checkBox = (CheckBox)e.OriginalSource;
                    Task task = checkBox.DataContext as Task;
                    task.TaskStatus = false;
                    DataDB.entities.Entry(task).State = EntityState.Modified;
                    DataDB.entities.SaveChanges();
                    DataTask();

                }
                else
                {
                    CheckBox check = (CheckBox)e.OriginalSource;
                    check.IsChecked = true;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Visible;

            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 900; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
