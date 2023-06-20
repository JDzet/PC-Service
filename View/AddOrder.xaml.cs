﻿using MahApps.Metro.Controls;
using PC_Service.ClassProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        private EntitiesMain dbContext = new EntitiesMain();
        private Orders new_order = new Orders();
        private Client new_client = new Client();
        

        public AddOrder()
        {
            InitializeComponent();
            DataContext = new_order;
            Data();
            Date();


        }

        public void Data() 
        {
            
            
                CbUser.ItemsSource = dbContext.Client.ToList();
                cbDevice.ItemsSource = dbContext.DeviceType.ToList();
                cbMaster.ItemsSource = dbContext.User.Where(x => x.Role.RoleId == 1).ToList();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

                if (!CheckData()) 
                {
                     return;
                }




                var user = dbContext.Client.FirstOrDefault(x => x.ClientName == CbUser.Text);
                if (user == null)
                {
                    new_client.ClientName = CbUser.Text;
                    new_client.PhoneNumber = tbPhone.Text;
                    new_client.ClientAddress = tbAdress.Text;
                    new_client.ClientEmail = tbEmail.Text;
                    dbContext.Client.Add(new_client);
                    dbContext.SaveChanges();
                }



                var usernow = dbContext.Client.FirstOrDefault(x => x.ClientName == CbUser.Text);
                 DeviceType device = cbDevice.SelectedItem as DeviceType;

                if (new_order.OrderID == 0)
                {
                    new_order.Client = usernow.ClientId;
                    new_order.Status = dbContext.Status.First();
                    var id = dbContext.User.FirstOrDefault(x => x.UserId == 2);
                    new_order.UserReceipt = id.UserId;
                    new_order.DeviceType = device.DeviceTypeId;
                    new_order.ReceiptTime = dpReceiptTime.SelectedDate.Value;
                    new_order.Deadline = dpDeadLine.SelectedDate.Value;
                    dbContext.Orders.Add(new_order);
                    dbContext.SaveChanges();
                    MessageBox.Show("Заказ добавлен");
                    this.Close();
                }

                 var dock = new WordHelper("Dock.docx"); //Работа с документом 

                InformationService informationService = dbContext.InformationService.FirstOrDefault();
                new_order = dbContext.Orders.OrderByDescending(x => x.OrderID).FirstOrDefault();


                var items = new Dictionary<string, object>
                 {
                     {"<NAME SERVICE>", informationService.Name},
                     {"<ADDRESS>", informationService.Address},
                     {"<EMAIL>", informationService.Email},
                     {"<NUMBER> ", new_order.OrderID + " "},
                     {"<DATATime>", new_order.ReceiptTime},
                     {"<CLIENT>", usernow.ClientName},
                     {"<PRICE>", new_order.Price},
                     {"<ORDER>", new_order.BrandDevice1.BrandName + " " + new_order.ModelDevice},
                     {"<NES>", new_order.Malfunction},
                     {"<COMPLIT>", new_order.Equipment},
                     {"<OUTFIT>", new_order.AppearanceDevice},
                     {"<NOTE>", tbNote.Text},
                     {"<RosCLIETN2>", usernow.ClientName},
                     {"<RosUSER2>", UserAuthorization.Worker.UserName},
                 };


                dock.Process(items);

        }


        public bool CheckData() 
        {

            var fieldsToCheck = new List<Control> 
            {
                CbUser, tbPhone, tbEmail, cbDevice, cbBrand,
                tbModel, tbMalfunction, cbMaster, tbPrice
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


        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Client client)
                {
                    tbPhone.Text = client.PhoneNumber;
                    tbEmail.Text = client.ClientEmail;
                    tbAdress.Text = client.ClientAddress;
                }
                else    // Обработка случая, когда выбранный элемент равен null
                {
                
                    tbPhone.Text = string.Empty;
                    tbEmail.Text = string.Empty;
                    tbAdress.Text = string.Empty;
                }
            }
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void BtAddType_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Visible;
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private void BtAddBrand_Click(object sender, RoutedEventArgs e)
        {
            FormContainerBrand.Visibility = Visibility.Visible;


            CbBrandAdd.ItemsSource = dbContext.DeviceType.ToList();
            
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainerBrand.BeginAnimation(Border.WidthProperty, animation);

        }

        private void cbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbBrand.SelectedIndex = -1;
            DeviceType device = (sender as ComboBox).SelectedItem as DeviceType;

            try
            {
                    cbBrand.ItemsSource = dbContext.BrandDevice.Where(x => x.Type == device.DeviceTypeId).ToList();
     
            }
            catch 
            {

            }   
        }

        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
        }

        private void WatchtCasesBrand_Click(object sender, RoutedEventArgs e)
        {
            FormContainerBrand.Visibility = Visibility.Collapsed;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
                cbBrand.SelectedIndex = -1;
                DeviceType device = new DeviceType()
                {
                    NameDeviceType = TbName.Text
                };
                dbContext.DeviceType.Add(device);
                dbContext.SaveChanges();
                MessageBox.Show("Устройство добавлено");
                FormContainer.Visibility = Visibility.Collapsed;
                Data();
                TbName.Text = null;

        }

        private void BrandAdd_Click(object sender, RoutedEventArgs e)
        {
                cbBrand.SelectedIndex = -1;
            
            
                DeviceType device = CbBrandAdd.SelectedItem as DeviceType;
                BrandDevice brand = new BrandDevice
                {
                    BrandName = TbNameBrand.Text,
                    Type = device.DeviceTypeId
                };
                dbContext.BrandDevice.Add(brand);
                dbContext.SaveChanges();
                MessageBox.Show("Бренд добавлен");
                FormContainerBrand.Visibility = Visibility.Collapsed;
                TbNameBrand.Text = null;
                CbBrandAdd.SelectedIndex = -1;
                DeviceType deviceMain = cbDevice.SelectedItem as DeviceType;
                if (deviceMain != null) 
                {
                    cbBrand.ItemsSource = dbContext.BrandDevice.Where(x => x.Type == deviceMain.DeviceTypeId).ToList();
                }
               
        }

        private void dpDeadLine_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void dpReceiptTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDeadLine != null) 
            {
                Date();
            }

        }

        public void Date() 
        {
            if (dpDeadLine.SelectedDate.HasValue && dpDeadLine.SelectedDate < dpReceiptTime.SelectedDate)
            {
                // Если выбранная дата во втором DatePicker раньше, чем в первом DatePicker,
                // устанавливаем минимальную дату второго DatePicker на дату из первого DatePicker.
                dpDeadLine.SelectedDate = dpReceiptTime.SelectedDate;
            }

            // Устанавливаем минимальное значение второго DatePicker равным выбранной дате в первом DatePicker.
            dpDeadLine.DisplayDateStart = dpReceiptTime.SelectedDate;
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true; // Отменяет ввод символа, если он не является цифрой.
                    return;
                }
            }

            // Дополнительная проверка на положительные числа
            if (tbPrice.Text.Length == 0 && e.Text == "0")
            {
                e.Handled = true; // Отменяет ввод нуля в начале, если положительные числа разрешены.
                return;
            }
        }

       
    }
}
