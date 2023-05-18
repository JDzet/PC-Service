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
using System.Windows.Shapes;

namespace PC_Service.Pages.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для WindowCountProducReg.xaml
    /// </summary>
    public partial class WindowCountProducReg : Window
    {

        List listReturn;
        public WindowCountProducReg(Product product)
        {
            InitializeComponent();

            TextProduct.Text = product.ProductName.ToString();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            double price, count, amount;
            price = double.Parse(TbPrice.Text);
            count = double.Parse(TbCount.Text);
            amount = price * count;

            

        }

        private void TbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }
    }
}
