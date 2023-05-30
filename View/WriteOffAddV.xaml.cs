using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using static PC_Service.View.RegistrationAdd;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для WriteOffAddV.xaml
    /// </summary>
    public partial class WriteOffAddV : Window
    {
        private ObservableCollection<TestData> testDatas;

        public WriteOffAddV()
        {
            InitializeComponent();
            DataWrite();
            
        }

        public void DataWrite() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CbWarehouse.ItemsSource = DataDB.entities.WarehouseService.ToList();
                testDatas = new ObservableCollection<TestData>();
                DataGridProduct.ItemsSource = testDatas;
            }

        }



        private int selectedWarehouseID; // получаем выбранный склад 
        private void CbWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                WarehouseService selectedWarehouse = CbWarehouse.SelectedItem as WarehouseService;
                selectedWarehouseID = selectedWarehouse?.WarehouseID ?? 0; // Сохраняем ID выбранного склада
                CbNameProduct.SelectedIndex = -1;
                LoadProductsByWarehouse(selectedWarehouseID); // Выполняем загруз
            
        }

       
        private void LoadProductsByWarehouse(int warehouseID)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                CbNameProduct.ItemsSource = DataDB.entities.ProductRemnants.Where(x => x.RemanantsWarehouse == warehouseID)
                     .Select(x => x.Product.ProductName)
                     .ToList();
            }
        }


        private void CbNameProduct_SelectionChanged(object sender, SelectionChangedEventArgs e) // добовление товара в таблицу при выборе товара
        {
          
                using (DataDB.entities = new EntitiesMain())
                {
                    var selectedProductName = CbNameProduct.SelectedItem;
                    ProductRemnants prod = DataDB.entities.ProductRemnants.FirstOrDefault(x => x.Product.ProductName == selectedProductName && x.RemanantsWarehouse == selectedWarehouseID);
                    if (prod == null)
                    { return; }
                    if (testDatas != null && testDatas.Any(data => data.Column1 == prod.Product.ProductName && data.Column4 == prod.WarehouseService.WarehouseName))
                        MessageBox.Show("Товар уже добавлен в таблицу");
                    else
                    {
                        testDatas.Add(new TestData() { Column1 = prod.Product.ProductName.ToString(), Column2 = prod.RemnantsQuantity.ToString(), Column3 = 0, Column4 = prod.WarehouseService.WarehouseName });
                        CbWarehouse.IsEnabled = false;
                    }
                }
           
           

        }

        public class TestData : INotifyPropertyChanged
        {


            private string column1;
            private string column2;
            private int column3;
            private string column4;
            


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
                    }
                }
            }

            public int Column3
            {
                get { return column3; }
                set
                {
                    if (column3 != value)
                    {
                        column3 = value;
                        OnPropertyChanged(nameof(Column3));
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

        } // Класс для работы с таблицой

        private void WriteOff_Click(object sender, RoutedEventArgs e) // действия при списание 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                ProductWriteOff writeOff = new ProductWriteOff();
                writeOff.Date = DateTime.Now;
                writeOff.WriteUser = UserAuthorization.Worker.UserId;
                writeOff.WriteWarehouse = selectedWarehouseID;
                writeOff.Note = TbNote.Text;

                try
                {
                    DataDB.entities.ProductWriteOff.Add(writeOff);
                    DataDB.entities.SaveChanges();



                    foreach (TestData item in DataGridProduct.Items)
                    {
                        Product product = DataDB.entities.Product.FirstOrDefault(x => x.ProductName == item.Column1);
                        ProductWriteOff productWriteOff = DataDB.entities.ProductWriteOff.OrderByDescending(x => x.WriteOffID).FirstOrDefault();
                        ProductWriteOffHistory writeOffHistory = new ProductWriteOffHistory()
                        {
                            ProductW = product.ProductID,
                            ProductWQuantity = item.Column3,
                            ProductWriteOff = productWriteOff.WriteOffID
                        };

                        DataDB.entities.ProductWriteOffHistory.Add(writeOffHistory);

                        ProductRemnants Remn = DataDB.entities.ProductRemnants.FirstOrDefault(x => x.RemnantsProduct == product.ProductID && x.RemanantsWarehouse == selectedWarehouseID);
                        if (Remn != null) // если такой товар есть, то берем и уменьшаем коллчество
                        {
                            Remn.RemnantsQuantity -= item.Column3;
                            DataDB.entities.Entry(Remn).State = EntityState.Modified;
                            if (Remn.RemnantsQuantity == 0) 
                            {
                                DataDB.entities.Entry(Remn).State = EntityState.Deleted;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Товара нет");
                        }
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Списание успешно");
                    }
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }

            }
            this.Close();
        }

        private void BtDellDate_Click(object sender, RoutedEventArgs e)
        {
            testDatas = new ObservableCollection<TestData>();
            CbWarehouse.IsEnabled = true;
            DataGridProduct.ItemsSource = testDatas;
            CbNameProduct.SelectedIndex = -1;
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            TestData data = (sender as Button).DataContext as TestData;
            bool confirmed = MessageBox.Show("Вы точно хотите удалить этот товар", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (confirmed)
            {
                testDatas.Remove(data);
                CbWarehouse.IsEnabled = true;
                CbNameProduct.SelectedIndex = -1;
            }
            else
            {

            }
        }


        private void DataGridTextColumn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
            {
                e.Handled = true; // Отменить обработку символа, если он не является цифрой
            }
        }

        private bool IsNumericInput(string text)
        {
            return text.All(char.IsDigit); // Проверить, является ли текст только цифрами
        }

        private void DataGridProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) // проверка введеного количества списания
        {
            if (e.Column.DisplayIndex == 2) // Проверяем, что это вторая колонка (индекс 2)
            {
                var editedCell = e.EditingElement as TextBox;
                var editedRow = e.Row.Item as TestData; 

                if (editedCell != null && editedRow != null)
                {
                    // Получаем значение второй ячейки
                    int editedCellValue = int.Parse(editedCell.Text);

                    // Получаем значение первой ячейки из связанного свойства в классе TestData
                    int firstCellValue = int.Parse(editedRow.Column2);

                        // Проверяем, что значение списание больше количества на складе
                        if (firstCellValue < editedCellValue)
                        { 
                            MessageBox.Show("Товара на складе меньше чем вы хотите списать");
                            editedCell.Text = firstCellValue.ToString();
                        }
                }
            }
        }
    }
}
