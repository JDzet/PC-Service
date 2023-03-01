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

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            MainFrame.Content = new Settings();
        }

    

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Settings(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Settings();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
         MainFrame.Content = new Order();
        }
    }
}
