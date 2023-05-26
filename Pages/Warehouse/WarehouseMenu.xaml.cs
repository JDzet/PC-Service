using PC_Service.Pages.Warehouse;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarehouseMenu.xaml
    /// </summary>
    public partial class WarehouseMenu : Page
    {
     
        public WarehouseMenu()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (MainTabControl.SelectedIndex)
            {
                case 0:
                    WarehouseRemains page = new WarehouseRemains();
                    RemainsFrame.Content = page;
                    break;
                case 1:
                    RegistrationFrame.Content = new WarehouseRegistration();
                    break;
                case 2:
                    WriteOffFrame.Content = new WarehouseWriteOff();
                    break;
                case 3:
                     ProductFrame.Content = new WarehouseProducts();
                    break;
            }

        }


    }
}
