using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
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
    /// Логика взаимодействия для RoleAndUser.xaml
    /// </summary>
    public partial class RoleAndUser : Page
    { EntitiesMain entities = new EntitiesMain();
        public RoleAndUser()
        {
            InitializeComponent();
            DataGridUser.ItemsSource = entities.User.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser((sender as Button).DataContext as User, entities);
            addUser.ShowDialog();



            DataGridUser.ItemsSource = null;
            DataGridUser.ItemsSource = entities.User.ToList();
        }
       

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser(null, entities);
            addUser.ShowDialog();
            DataGridUser.ItemsSource = null;
            DataGridUser.ItemsSource = entities.User.ToList();
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var UserForRemoving = DataGridUser.SelectedItems.Cast<User>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить этих пользователей{UserForRemoving.Count()} элементов", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
            {
                try
                {
                    entities.User.RemoveRange(UserForRemoving);
                    entities.SaveChanges();
                    MessageBox.Show("Данные удалены");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            DataGridUser.ItemsSource = null;
            DataGridUser.ItemsSource = entities.User.ToList();

        }
    }
}
