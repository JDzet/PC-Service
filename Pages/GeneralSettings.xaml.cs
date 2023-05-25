using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для GeneralSettings.xaml
    /// </summary>
    public partial class GeneralSettings : Page
    {
        ApiYandex api = new ApiYandex();
        public GeneralSettings()
        {
            InitializeComponent();
            DataSettings();
        }

        public void DataSettings() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                InformationService informationService = DataDB.entities.InformationService.FirstOrDefault();
                DataContext = informationService;
            }


        }

        private async void CbAdres_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = CbAdres.Text;
            List<string> filteredAdress = await api.GetAutocompleteAddresses(searchText);
            CbAdres.ItemsSource = filteredAdress;
            CbAdres.IsDropDownOpen = true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                InformationService _service = (InformationService)this.DataContext;
                _service.ServiceID = 1;

                try 
                {
                    DataDB.entities.Entry(_service).State = EntityState.Modified;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Информация обновлена");
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
               

            }
        }
    }
}
