using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
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

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private Client _client;
        ApiYandex api = new ApiYandex();

        public AddClient(Client client)
        {
            InitializeComponent();
            _client = client ?? new Client();
            DataContext = _client;
            CheckBSapline.IsChecked = _client.Supplier;
        }

        public bool CheckData()
        {

            var fieldsToCheck = new List<Control>
            {
                tbName,tbAdress
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


        private void DataBaseClientAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                return;
            }


            _client = (Client)this.DataContext;

            if(CheckBSapline.IsChecked == true) 
            {
                _client.Supplier = true;
            }
            else { _client.Supplier = false; }

            using (DataDB.entities = new EntitiesMain()) 
            {
                if (_client.ClientId == 0)
                {
                    try
                    {
                        DataDB.entities.Client.Add(_client);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var error in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in error.ValidationErrors)
                            {
                                MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
                else
                {
                    try // если идет редактирование пользователя, а не добавление 
                    {
                        DataDB.entities.Entry(_client).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }
            this.Close();
        }

       

        private async void tbAdress_TextChanged(object sender, TextChangedEventArgs e) // работа с api яндекса
        {
            string searchText = CbNaem.Text;
            List<string> filteredAdress = await api.GetAutocompleteAddresses(searchText);
            CbNaem.ItemsSource = filteredAdress;
            CbNaem.IsDropDownOpen = true;
        }
        
    }
}
