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
        }

        private void DataBaseClientAdd_Click(object sender, RoutedEventArgs e)
        {
            
             _client = (Client)this.DataContext;

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
