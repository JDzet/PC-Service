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
using PC_Service.View;

namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {

        Task task1 = new Task();
        public TasksPage()
        {
            InitializeComponent();
            DataTask();

        }

        public void DataTask()
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGridTaskAtWork.ItemsSource = DataDB.entities.Task.Include(x => x.User).Where(x=>x.TaskStatus == false).OrderByDescending(x=>x.TaskID).ToList();
                DataGridWorkCompletedk.ItemsSource = DataDB.entities.Task.Include(x => x.User).Where(x => x.TaskStatus == true).OrderByDescending(x => x.TaskID).ToList();
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
        }// выполненые задачи

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

        }// пересос из выполененых задач

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask add = new AddTask(null);
            add.Owner = Window.GetWindow(this);
            add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            add.ShowDialog();
            DataTask();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            task1 = (sender as Button).DataContext as Task;
            AddTask add = new AddTask(task1);
            add.ShowDialog();
            DataTask();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            task1 = (sender as Button).DataContext as Task;

            bool confirmed = MessageBox.Show("Удалить задачу", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (confirmed) 
            {
                using (DataDB.entities = new EntitiesMain()) 
                {
                    DataDB.entities.Entry(task1).State = EntityState.Deleted;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Задача удалена");
                }
                DataTask();
            }
        }
    }
}
