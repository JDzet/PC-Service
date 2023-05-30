using PC_Service.ClassProject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text.RegularExpressions;
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
        private readonly EntitiesMain _entities;
        private readonly User _user;

        public AddUser(User user, EntitiesMain entities)
        {
            InitializeComponent();
            _entities = entities;
            _user = user ?? new User();
            DataContext = _user;
            CbRole.ItemsSource = _entities.Role.ToList();




        }

        private void DataBaseUserAdd_Click(object sender, RoutedEventArgs e)
        {
            Protection(TbPassword.Text);
        }


        public void BtRandom_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordGenerator.GeneratePassword();
            TbPassword.Text = password.ToString();
        }

        public int Protection(String str) 
        {
            bool isValid = Regex.IsMatch(str, @"^(?=.*[A-Z])(?=.*\d{2}).{8}$");
            if (isValid)
            {
                if (_user.UserId == 0)
                {

                    _entities.User.Add(_user);
                    _entities.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                    this.Close();
                }
                else
                {
                    try // если идет редактирование пользователя, а не добавление 
                    {

                        _entities.Entry(_user).State = EntityState.Modified;
                        _entities.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    
                }
                return 1;
            }
            else
            {
                MessageBox.Show("Пароль не соответсвует требованиям.\nВоспользуйтесь генератором пароля");
                return 0;
               
            }
        }
    }


}
