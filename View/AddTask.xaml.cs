using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        Task task;
        int id;
        public AddTask(Task _task)
        {
            InitializeComponent();
            task = _task ?? new Task();
            id = task.TaskID;
            DataContext = task;
            DataAddTask();
        }

        public void DataAddTask() 
        {
            using (DataDB.entities = new EntitiesMain())
            {
                CbUser.ItemsSource = DataDB.entities.User.ToList();
                CbUser.SelectedValuePath = "UserId";
                CbUser.SelectedValue = task.TaskUser;
            }
            if (id == 0) 
            {
                task.Date = DateTime.Now;
            }
            if (task.TaskStatus)
            {
                TbName.IsEnabled = false;
                TbDescrip.IsEnabled = false;
                PickerDate.IsEnabled = false;
                CbUser.IsEnabled = false;
                BSave.IsEnabled = false;
            }

        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)CbUser.SelectedItem;
            task = new Task()
            {
                TaskID = task.TaskID,
                TaskName = TbName.Text,
                Description = TbDescrip.Text,
                Date = PickerDate.SelectedDate.Value,
                TaskUser = user.UserId,
                TaskStatus = false
            };



            using (DataDB.entities = new EntitiesMain()) 
            {
                try
                {
                    if (task.TaskID == 0)
                    {
                        DataDB.entities.Task.Add(task);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Задача добавлена");
                    }
                    else
                    {
                        DataDB.entities.Entry(task).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Задача обновлена");
                    }
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
            }
           
        }
    }
}
