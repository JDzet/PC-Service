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
using System.Xml.Linq;

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
            if (!CheckData())
            {
                return;
            }

            Protection(TbPassword.Text);
        }


        public void BtRandom_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordGenerator.GeneratePassword();
            TbPassword.Text = password.ToString();
        }

        public bool CheckData()
        {

            var fieldsToCheck = new List<Control>
            {
                tbName,tbPhone,tbAdress,TbLogin,TbPassword,CbRole
            };

            bool allFieldsFilled = true;

            // Проверяем каждое поле в списке
            foreach (var field in fieldsToCheck)
            {
                // Проверяем, заполнено ли поле
                if (field is System.Windows.Controls.TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    allFieldsFilled = false;
                    textBox.BorderBrush = Brushes.IndianRed;
                }
                else if (field is ComboBox comboBox && comboBox.SelectedItem == null)
                {
                    allFieldsFilled = false;
                    comboBox.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    field.ClearValue(Control.BorderBrushProperty);
                }
            }

            if (!allFieldsFilled)
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }

            return true;
        }


        public int Protection(String str) 
        {

            

            bool isValid = Regex.IsMatch(str, @"^(?=.*[A-Z])(?=.*\d{2}).{8}$");
            if (isValid)
            {
                if (_user.UserId == 0)
                {
                    _user.Password = TbPassword.Text;
                    _entities.User.Add(_user);
                    _entities.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                    this.Close();
                }
                else
                {
                    try // если идет редактирование пользователя, а не добавление 
                    {
                        _user.Password = TbPassword.Text;
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
