using PC_Service.Pages.Warehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
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
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using System.Web.UI;
using Control = System.Windows.Controls.Control;
using PC_Service.Pages;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationAdd.xaml
    /// </summary>
    public partial class RegistrationAdd : Window
    {
        private ObservableCollection<TestData> testDatas;
        UserAuthorization UserAuthorization = new UserAuthorization();
      
        decimal totalAmount = 0;
      
        public RegistrationAdd()
        {
            InitializeComponent();
            testDatas = new ObservableCollection<TestData>();
            
            DataRegistation();
            DataGridProduct.ItemsSource = testDatas;
        }

    

        public void DataRegistation() //Заполнения полей и получение данных из бд
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CBClient.ItemsSource = DataDB.entities.Client.Where(x => x.Supplier == true).ToList();
                CBProduct.ItemsSource = DataDB.entities.Product.ToList();
                Warehouse.ItemsSource = DataDB.entities.WarehouseService.ToList();
            }

        }

        public void Disable(Control control) // блокировка элементов формы
        {
            control.IsEnabled = false;
            foreach (var child in LogicalTreeHelper.GetChildren(control).OfType<Control>())
            {
                Disable(child);
            }

        }

        


        private void BRegistration_Click(object sender, RoutedEventArgs e) // обработка добавления оприходования
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                RegistrationProduct regProd = new RegistrationProduct();
                regProd.Date = DateTime.Now;
                regProd.InvoiceNumber = $"{TBInvoiceNumber.Text} от {DataPicekt.Text}";
                regProd.RegUser = UserAuthorization.Worker.UserId;

                Client selectClient = CBClient.SelectedItem as Client;
                regProd.RegClient = selectClient.ClientId;

                WarehouseService selectWarehouse = Warehouse.SelectedItem as WarehouseService;
                regProd.RegWarehouse = selectWarehouse.WarehouseID;

                regProd.Note = TBNote.Text;
                regProd.RegAmount = totalAmount;


                try
                {
                    DataDB.entities.RegistrationProduct.Add(regProd);
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("нет");
                }
                // создание и оприходования 



                foreach (TestData item in DataGridProduct.Items)  // создания и добовление товара в бд
                {
                    Product product = DataDB.entities.Product.FirstOrDefault(x => x.ProductName == item.Column1);
                    RegistrationProduct registrationProductLast = DataDB.entities.RegistrationProduct.OrderByDescending(x => x.RegistrationID).FirstOrDefault();
                    ProductHistoryRegistration productHistory = new ProductHistoryRegistration()
                    {
                        ProductHName = product.ProductID, // Название товара
                        ProductHPForOne = decimal.Parse(item.Column2), // Цена за штуку
                        ProductHAmount = decimal.Parse(item.Column4), // Общая стоимость
                        ProductHQuantity = int.Parse(item.Column3), // Количество
                        RegProduct = registrationProductLast.RegistrationID // Связь с определенным оприходованием
                    };

                    DataDB.entities.ProductHistoryRegistration.Add(productHistory);


                    ProductRemnants Remprod = DataDB.entities.ProductRemnants.FirstOrDefault(x => x.RemnantsProduct == product.ProductID && x.RemanantsWarehouse == selectWarehouse.WarehouseID); // получение товара из таблицы остатков
                    if (Remprod != null) // если такой товар есть, то берем и увеличиваем коллчество
                    {
                        Remprod.RemnantsQuantity += int.Parse(item.Column3);
                        DataDB.entities.Entry(Remprod).State = EntityState.Modified;
                    }
                    else // если товара нет, то добавляем в таблицу новый
                    {
                        ProductRemnants remnants = new ProductRemnants()
                        {
                            RemnantsProduct = product.ProductID,
                            RemnantsQuantity = int.Parse(item.Column3),
                            RemanantsWarehouse = selectWarehouse.WarehouseID
                        };
                        DataDB.entities.ProductRemnants.Add(remnants); 
                    }
                    DataDB.entities.SaveChanges();
                }
            }
             
        }
        

        private void CBProduct_SelectionChanged(object sender, SelectionChangedEventArgs e) // оброботчик выбора товара и занисение в таблицу 
        {

            Product prod = CBProduct.SelectedItem as Product;
            if (prod == null) 
            { return; }
            if (testDatas.Any(data => data.Column1 == prod.ProductName))
                MessageBox.Show("Товар уже добавлен в таблицу");
            else
                testDatas.Add(new TestData() { Column1 = prod.ProductName.ToString(), Column2 = "", Column3 = "", Column4 = "" });
        }

        
        public class TestData : INotifyPropertyChanged
        {
            private string column1;
            private string column2;
            private string column3;
            private string column4;
            public static decimal amount { get; set; }
            

            public string Column1
            {
                get { return column1; }
                set
                {
                    if (column1 != value)
                    {
                        column1 = value;
                        OnPropertyChanged(nameof(Column1));
                    }
                }
            }

            public string Column2
            {
                get { return column2; }
                set
                {
                    if (column2 != value)
                    {
                        column2 = value;
                        OnPropertyChanged(nameof(Column2));
                        CalculateTotal();
                    }
                }
            }

            public string Column3
            {
                get { return column3; }
                set
                {
                    if (column3 != value)
                    {
                        column3 = value;
                        OnPropertyChanged(nameof(Column3));
                        CalculateTotal();
                    }
                }
            }

            public string Column4
            {
                get { return column4; }
                set
                {
                    if (column4 != value)
                    {
                        column4 = value;
                        OnPropertyChanged(nameof(Column4));
                    }
                }
            }

          

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            
            private void CalculateTotal()
            {
                
                if (double.TryParse(Column2, out double price) && double.TryParse(Column3, out double quantity))
                {
                    amount = 0;
                    Column4 = (price * quantity).ToString();
                    amount += decimal.Parse(Column4);
                    
                }
                else
                {
                    Column4 = "";
                }


            }

        } // Класс для работы с таблицой

        private void BtDellDate_Click(object sender, RoutedEventArgs e) // очистка таблицы
        {
            testDatas = new ObservableCollection<TestData>();
            DataGridProduct.ItemsSource = testDatas;
        }

        private void DataGridProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            totalAmount = 0;
            foreach (var item in testDatas)
            {
                if (!string.IsNullOrEmpty(item.Column4))
                {
                    totalAmount += decimal.Parse(item.Column4);
                }
            }

            TblockPrice.Text = totalAmount.ToString() + " Р";
        } // счёт суммы

        private void CBProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                string serchText = CBProduct.Text;
                if (!string.IsNullOrEmpty(serchText))
                {
                    CBProduct.ItemsSource = DataDB.entities.Product.Where(x => x.ProductName.Contains(serchText)).ToList();
                    CBProduct.IsDropDownOpen = true;
                }
                else 
                {
                    CBProduct.ItemsSource = DataDB.entities.Product.ToList();
                }

                
            }
               
        }

    }
}
