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

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationAdd.xaml
    /// </summary>
    public partial class RegistrationAdd : Window
    {
        Request req = new Request();
        WindowCountProducReg CountReg;
        private ObservableCollection<TestData> testDatas;
        UserAuthorization UserAuthorization = new UserAuthorization();
        MyData data;
        EntitiesMain entities;
        decimal totalAmount = 0;
        public RegistrationAdd()
        {
            InitializeComponent();
            entities = req.entities;
            testDatas = new ObservableCollection<TestData>();

            data = new MyData();
            data.Client = entities.Client.ToList();
            data.Product = entities.Product.ToList();
            data.Warehouse = entities.Warehouse.ToList();

            DataContext = data;
            DataGridProduct.ItemsSource = testDatas;



        }

        public class MyData  
        {
            public List<Client> Client { get; set; }
            public List<Product> Product { get; set; }
            public List<Warehouse> Warehouse { get; set; }
        }


        private void DatePicker_GotStylusCapture(object sender, StylusEventArgs e)
        {
           
            



        }



        private void BRegistration_Click(object sender, RoutedEventArgs e)
        {
           
            RegistrationProduct regProd = new RegistrationProduct();
            regProd.Date = DateTime.Now;
            regProd.InvoiceNumber = $"{TBInvoiceNumber.Text} от {DataPicekt.Text}";
            regProd.RegUser = UserAuthorization.Worker.UserId;

            Client selectClient = CBClient.SelectedItem as Client;
            regProd.RegClient = selectClient.ClientId;

            Warehouse selectWarehouse = Warehouse.SelectedItem as Warehouse;
            regProd.RegWarehouse = selectWarehouse.WarehouseID;

            regProd.Note = TBNote.Text;
            regProd.RegAmount = totalAmount;


            try
            {
                entities.RegistrationProduct.Add(regProd);
                entities.SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception)
            {
                MessageBox.Show("НЕт");
            }




            foreach (TestData item in DataGridProduct.Items)
            {
                Product product = entities.Product.FirstOrDefault(x => x.ProductName == item.Column1);
                RegistrationProduct registrationProductLast = entities.RegistrationProduct.OrderByDescending(x => x.RegistrationID).FirstOrDefault();
                ProductHistoryRegistration productHistory = new ProductHistoryRegistration()
                {
                    ProductHName = product.ProductID, // Название товара
                    ProductHPForOne = decimal.Parse(item.Column2), // Цена за штуку
                    ProductHAmount = decimal.Parse(item.Column4), // Общая стоимость
                    ProductHQuantity = int.Parse(item.Column3), // Количество
                    RegProduct = registrationProductLast.RegistrationID                                  // Дополнительные свойства, если есть
                };

                entities.ProductHistoryRegistration.Add(productHistory);
                entities.SaveChanges();


                ProductRemnants Remprod = entities.ProductRemnants.FirstOrDefault(x => x.RemnantsProduct == product.ProductID);
                if(Remprod != null)
                {
                    Remprod.RemnantsQuantity += int.Parse(item.Column3);

                    entities.Entry(Remprod).State = EntityState.Modified;
                    entities.SaveChanges();

                }
                else
                {
                    ProductRemnants remnants = new ProductRemnants()
                    {
                        RemnantsProduct = product.ProductID,
                        RemnantsQuantity = int.Parse(item.Column3),

                    };

                    entities.ProductRemnants.AddOrUpdate(x => x.RemnantsProduct, remnants);
                    entities.SaveChanges();
                }
                

               



            }






        }
        

        private void CBProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Product prod = CBProduct.SelectedItem as Product;
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

        }

        private void BtDellDate_Click(object sender, RoutedEventArgs e)
        {
            testDatas = new ObservableCollection<TestData>();
            DataGridProduct.ItemsSource = testDatas;
        }

        private void DataGridProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            decimal totalAmount = 0;
            foreach (var item in testDatas)
            {
                if (!string.IsNullOrEmpty(item.Column4))
                {
                    totalAmount += decimal.Parse(item.Column4);
                }
            }

            TblockPrice.Text = totalAmount.ToString() + " Р";
        }
    }
}
