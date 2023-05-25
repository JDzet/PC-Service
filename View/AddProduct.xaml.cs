using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {

        private Product product;
        public AddProduct(Product _prod)
        {
            InitializeComponent();
            product = _prod ?? new Product();
            DataContext = product;
        }

       

        private void DataBaseProductAdd_Click(object sender, RoutedEventArgs e)
        {
            product = (Product)this.DataContext;

            

            using (DataDB.entities = new EntitiesMain())
            {
                if (product.ProductID == 0)
                {
                    try
                    {
                        DataDB.entities.Product.Add(product);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Товар сохранен");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var error in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in error.ValidationErrors)
                            {
                                MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
                else
                {
                    try // если идет редактирование пользователя, а не добавление 
                    {
                        DataDB.entities.Entry(product).State = EntityState.Modified;
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Товар сохранен");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }
            this.Close();
        }
    }
}
