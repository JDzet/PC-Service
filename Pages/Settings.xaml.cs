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

namespace PC_Service
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
          
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
          SettingsFrame.Content = new RoleAndUser();
        }

        private void GeneralSettings_Clik(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = new GeneralSettings();
        }
    }
}
