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
    /// Логика взаимодействия для WriteOffInfo.xaml
    /// </summary>
    public partial class WriteOffInfo : Window
    {
        ProductWriteOff _writeOff = new ProductWriteOff();
        public WriteOffInfo(ProductWriteOff writeOff)
        {
            InitializeComponent();
            this._writeOff = writeOff;
            DataContext = _writeOff;
            DataWrite();
        }

        public void DataWrite() 
        {
            using(DataDB.entities = new EntitiesMain()) 
            {
                DataGridInfo.ItemsSource = DataDB.entities.ProductWriteOffHistory.Include(x=>x.Product).Where(x=>x.ProductWriteOff == _writeOff.WriteOffID).ToList();
            }
        }
    }
}
