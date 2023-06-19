﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting.Contexts;


namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeviceDirectoryPage.xaml
    /// </summary>
    public partial class DeviceDirectoryPage : Page
    {
        int id = 0;
        public DeviceDirectoryPage()
        {
            InitializeComponent();
            DataDevice();
        }

        public void DataDevice() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGridDevice.ItemsSource = DataDB.entities.DeviceType.ToList();
                CbBrand.ItemsSource = DataDB.entities.DeviceType.ToList();
            }
        }

        private void DataGridDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
                    using (DataDB.entities = new EntitiesMain())
                    {
                        DeviceType type = new DeviceType();
                        type = DataGridDevice.SelectedItem as DeviceType;
                         try 
                        {
                            DataGridBrand.ItemsSource = DataDB.entities.BrandDevice.Where(x => x.Type == type.DeviceTypeId).ToList();
                        }
                        catch { }
                       
                    }
          
            

        }


        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
            TbName.Text = null;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            EntitiesMain context = new EntitiesMain();
          
            

                DeviceType device = new DeviceType()
                {
                    DeviceTypeId = id,
                    NameDeviceType = TbName.Text
                };

                if (device.DeviceTypeId == 0)
                {
                    context.DeviceType.Add(device);
                    context.SaveChanges();
                    MessageBox.Show("Тип устройтсва добален");
                }
                else 
                {
                    context.Entry(device).State = EntityState.Modified;
                    context.SaveChanges();
                    MessageBox.Show("Изменение сохранено");
                }
                FormContainer.Visibility = Visibility.Collapsed;
                TbName.Text = null;
                DataGridDevice.ItemsSource = context.DeviceType.ToList();
                CbBrand.ItemsSource = context.DeviceType.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             id = 0;
            FormContainer.Visibility = Visibility.Visible;
            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private void BtnEditType_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            FormContainer.Visibility = Visibility.Visible;
            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);

            DeviceType device = (sender as Button).DataContext as DeviceType;
            id = device.DeviceTypeId;
            TbName.Text = device.NameDeviceType.ToString();
        }

        private void BtnDel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DeviceType device = (sender as Button).DataContext as DeviceType;
                using (DataDB.entities = new EntitiesMain())
                {
                    bool confirmed = MessageBox.Show("Вы уверены, что хотите удалить устройтво", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                    if (confirmed)
                    {
                        List<BrandDevice> brandDevices = DataDB.entities.BrandDevice.Where(x => x.Type == device.DeviceTypeId).ToList();
                        foreach (var brandDevice in brandDevices)
                        {
                            DataDB.entities.Entry(brandDevice).State = EntityState.Deleted;
                        }

                        DataDB.entities.Entry(device).State = EntityState.Deleted;
                        DataDB.entities.SaveChanges();
                        DataGridBrand.ItemsSource = null;
                        List<DeviceType> deviceTypes = DataDB.entities.DeviceType.ToList();
                        DataGridDevice.ItemsSource = deviceTypes;
                        CbBrand.ItemsSource = deviceTypes;
                    }
                    else
                    {

                    }
                }
               
            }
            catch 
            {
                MessageBox.Show("Один из брендов сейчас используется в заказе.\nЗакройте и удалите все заказы связанные с этим типом устройств","Внимание");
            }
           

        }





        // работа с брэндом
        private void BtModel_Click(object sender, RoutedEventArgs e)
        {
            id = 0;

            FormContainerBrand.Visibility = Visibility.Visible;
            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainerBrand.BeginAnimation(Border.WidthProperty, animation);
        }

        private void BrandAdd_Click(object sender, RoutedEventArgs e) // добавление, редакктирование в бд, брэнда устройства
        {
            using (DataDB.entities = new EntitiesMain())
            {

                DeviceType type = (DeviceType)CbBrand.SelectedValue;

                BrandDevice brand = new BrandDevice()
                {
                    BrandId = id,
                    BrandName = TbNameBrand.Text,
                    Type = type.DeviceTypeId
                    
                };

                if (brand.BrandId == 0)
                {
                    DataDB.entities.BrandDevice.Add(brand);
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Бренд устройства добален");
                }
                else
                {
                    DataDB.entities.Entry(brand).State = EntityState.Modified;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Изменение сохранено");
                }
                DataGridBrand.ItemsSource = DataDB.entities.BrandDevice.Where(x=>x.Type == brand.Type).ToList();
                FormContainerBrand.Visibility = Visibility.Collapsed;
                TbNameBrand.Text = null;
                CbBrand.SelectedIndex = -1;
            }
        }

        private void WatchtCasesBrand_Click(object sender, RoutedEventArgs e)
        {
            FormContainerBrand.Visibility = Visibility.Collapsed;
        }

        private void BtnDelModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrandDevice brand = (sender as Button).DataContext as BrandDevice;
                using (DataDB.entities = new EntitiesMain())
                {
                    bool confirmed = MessageBox.Show("Вы уверены, что хотите брэнд", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                    if (confirmed)
                    {
                        DataDB.entities.Entry(brand).State = EntityState.Deleted;
                        DataDB.entities.SaveChanges();
                        DataGridBrand.ItemsSource = DataDB.entities.BrandDevice.Where(x => x.Type == brand.Type).ToList();
                    }
                    else
                    {

                    }
                }
            }
            catch 
            {
                MessageBox.Show("Данный бренд устройства сейчас используется в заказе.\nЗакройте и удалите заказ перед удалением данного бренда", "Внимание");
            }
        }

        private void BtnEditModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FormContainerBrand.Visibility = Visibility.Visible;
            // Добавьте анимацию, чтобы панель выезжала вправо
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 200; // Ширина формы (можете настроить под свои нужды)
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainerBrand.BeginAnimation(Border.WidthProperty, animation);

            BrandDevice brand = (sender as Button).DataContext as BrandDevice;
            id = brand.BrandId;
            TbNameBrand.Text = brand.BrandName.ToString();
            CbBrand.SelectedValue = brand.Type;
        }
    }
}
