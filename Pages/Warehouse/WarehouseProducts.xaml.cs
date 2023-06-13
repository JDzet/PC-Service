using PC_Service.View;
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
using ControlzEx.Standard;
using System.Data.Entity;
using System.Windows.Media.Animation;


namespace PC_Service.Pages.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для WarehouseProducts.xaml
    /// </summary>
    public partial class WarehouseProducts : Page
    {
        public WarehouseProducts()
        {
            InitializeComponent();
            DataProduct();
        }

        public void DataProduct()
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGrid.ItemsSource = DataDB.entities.Product.ToList();
                ConutText.Text = "Всего товаров: " + DataGrid.Items.Count.ToString();
            }
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(null);
            addProduct.Owner = Window.GetWindow(this);
            addProduct.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addProduct.ShowDialog();
            DataProduct();
        }


        private void BtnEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Product product = (sender as Button).DataContext as Product;
            AddProduct addProduct = new AddProduct(product);
            addProduct.ShowDialog();
            DataProduct();
        }

        private void BtnDel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                Product product = (sender as Button).DataContext as Product;

                if (MessageBox.Show($"Вы точно хотите удалить этот товар", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataDB.entities.Entry(product).State = EntityState.Deleted;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Товар удален");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                DataProduct();
            }
        }
    }
}
