﻿using PC_Service.View;
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
    /// Логика взаимодействия для ProfilPage.xaml
    /// </summary>
    public partial class ProfilPage : Page
    {
        public ProfilPage()
        {
            InitializeComponent();
            TbName.Text = UserAuthorization.Worker.UserName.ToString();
            Role.Text = UserAuthorization.Worker.Role.RoleName.ToString();
        }

     
    }
}