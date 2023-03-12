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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Service
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public User new_user = new User();
        EntitiesMain entities = new EntitiesMain();

        public AddUser(User selectedUser)
        {
            InitializeComponent();
            if(selectedUser != null)
                new_user = selectedUser;


            DataContext = new_user;
            CbRole.ItemsSource = entities.Role.ToList();
         
   
        }

        private void DataBaseUserAdd_Click(object sender, RoutedEventArgs e)
        {
            if (new_user.UserId == 0)
                entities.User.Add(new_user);

            try
            {
                entities.SaveChanges();
                MessageBox.Show("Данные сохранены");
                this.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
