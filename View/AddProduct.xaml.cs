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
using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;

namespace PC_Service.View
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {

        private Product product;
        static byte[] imageData;
        public AddProduct(Product _prod)
        {
            InitializeComponent();
            product = _prod ?? new Product();
            DataContext = product;

            if (product.ProductPhoto != null) 
            {
                Photo();
            }
        }

        public void Photo() 
        {
            using (MemoryStream ms = new MemoryStream(product.ProductPhoto))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                BoxPhoto.Source = bitmapImage;
            }
        }

       

        private void DataBaseProductAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                return;
            }

            product = (Product)this.DataContext;

            

            using (DataDB.entities = new EntitiesMain())
            {
                if (product.ProductID == 0)
                {
                    try
                    {
                        if(fileDialog.FileName != "") 
                        {
                            byte[] imageData = File.ReadAllBytes(fileDialog.FileName);
                            product.ProductPhoto = imageData;
                        }
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
                        if (fileDialog.FileName != "") 
                        {
                            byte[] imageData = File.ReadAllBytes(fileDialog.FileName);
                            product.ProductPhoto = imageData;
                        }

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

        OpenFileDialog fileDialog = new OpenFileDialog();
        private void BoxPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fileDialog.ShowDialog() == true)
            {
                BoxPhoto.Source = new BitmapImage(new Uri(fileDialog.FileName));
                
            }
        }

        public bool CheckData()
        {

            var fieldsToCheck = new List<Control>
            {
                TbName
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
    }
}
