using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Data.Entity;
using System.Windows;
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
    /// Логика взаимодействия для Finance.xaml
    /// </summary>
    public partial class Finance : Page
    {
        public Finance()
        {
            InitializeComponent();
            DataFinance();
        }

        public void DataFinance() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                DataGridMain.ItemsSource = DataDB.entities.HistoryTransaction.Include(x=>x.Transactions).Include(x=>x.User1).ToList();
                Capital capital = DataDB.entities.Capital.FirstOrDefault();

                TbBalance.Text = "Общий баланс: " + capital.Amount.ToString();
            }
               
        }


        private void BtArrival_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction addTransaction = new AddTransaction("Приход денег", 1);
            addTransaction.Owner = Window.GetWindow(this);
            addTransaction.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addTransaction.ShowDialog();
            DataFinance();
        }

        private void Btspending_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction addTransaction = new AddTransaction("Расход" , 2);
            addTransaction.Owner = Window.GetWindow(this);
            addTransaction.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addTransaction.ShowDialog();
            DataFinance();
        }

        private void BtnDel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            HistoryTransaction transactions = (sender as Button).DataContext as HistoryTransaction;
            bool confirmed = MessageBox.Show("Удалить данные об операции", "Внимамние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            using (DataDB.entities = new EntitiesMain()) 
            {
                if (confirmed)
                {
                    DataDB.entities.Entry(transactions).State = EntityState.Deleted;
                    DataDB.entities.SaveChanges();
                    MessageBox.Show("Операция удалена");
                    DataFinance();
                }
                
                
            }
        }
    }
}
