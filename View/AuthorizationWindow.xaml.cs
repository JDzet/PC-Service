using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PC_Service.View;
using QRCoder;


namespace PC_Service
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        EntitiesMain entities = new EntitiesMain();
        MainMenu mainMenu = new MainMenu();
        UserAuthorization UserAuthorization = new UserAuthorization();
        public User userAutho { get; set; }


        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
                if (TextBoxLogin.Text != "" && TextBoxPasswrod.Text != "")
                {
                    userAutho = entities.User.FirstOrDefault(X => X.Login == TextBoxLogin.Text && X.Password == TextBoxPasswrod.Text);

                    if (userAutho != null)
                    {
                        MessageBox.Show($"Добро пожаловать {userAutho.UserName}");
                        mainMenu.Show();
                        UserAuthorization.Worker = userAutho;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароль или логин неверный");
                    }

                }
                else
                {
                    MessageBox.Show("Логин или пароль не введены");
                }
            

        }

    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://github.com/JDzet/PC-Service.git", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                var handle = qrCodeImage.GetHbitmap();
                QRImage.Source = Imaging.CreateBitmapSourceFromHBitmap(qrCodeImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    
    }
}
