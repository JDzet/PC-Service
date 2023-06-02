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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Service.Pages
{
    /// <summary>
    /// Логика взаимодействия для FinanceCategories.xaml
    /// </summary>
    public partial class FinanceCategories : Page
    {
        public FinanceCategories()
        {
            InitializeComponent();
            DataFinance();
        }

        public void DataFinance() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGrid.ItemsSource = DataDB.entities.Transactions.Include(x=>x.TransactionType).ToList();
            }
        }
    }
}
