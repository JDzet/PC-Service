﻿using PC_Service.Pages;
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
        UserAuthorization UserAuthorization = new UserAuthorization();
        
       

        public MainMenu()
        {
            InitializeComponent();
            MainFrame.Content = new Order();
            Doc.IsEnabled = false;
            
        }

    

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Settings(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.Worker.UserId != 1)
                MessageBox.Show("У вас нет прав для перехода к данной форме", "Внимание");
            else
                MainFrame.Content = new Settings();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
         MainFrame.Content = new Order();
        }

        private void ItemWarehouseMenu_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new WarehouseMenu();
        }

        private void ItemClient_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ClientPage();
        }

        private void FinanceMeny_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Finance();
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TasksPage();
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProfilPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
            bool confirmed = MessageBox.Show("Выйти из учётной записи ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (confirmed)
            {
                AuthorizationWindow authorization = new AuthorizationWindow();
                authorization.Show();
                this.Close();
            }

        }
    }
}
