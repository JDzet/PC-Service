using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
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
    /// Логика взаимодействия для RegistratoinInfo.xaml
    /// </summary>
    public partial class RegistratoinInfo : Window
    {
        RegistrationProduct _reg = new RegistrationProduct();
        public RegistratoinInfo(RegistrationProduct reg)
        {
            InitializeComponent();
            _reg = reg;
            DataContext = _reg;
            DataInfo();

        }

        public void DataInfo() 
        {
            using (DataDB.entities = new EntitiesMain())
            {
                DataGridInfo.ItemsSource = DataDB.entities.ProductHistoryRegistration.Include(x => x.Product).Where(x => x.RegProduct == _reg.RegistrationID).ToList();
            }
                
        }
    }
}
