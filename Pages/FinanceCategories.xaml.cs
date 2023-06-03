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
using System.Windows.Media.Animation;
using ControlzEx.Standard;

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
                CbType.ItemsSource = DataDB.entities.TransactionType.ToList();
                DataGrid.ItemsSource = DataDB.entities.Transactions.Include(x=>x.TransactionType).ToList();
            }
        }

        private void AddTransactionClick_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Visible;

            
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 250; // Ширина формы 
            animation.Duration = TimeSpan.FromSeconds(0.3);
            FormContainer.BeginAnimation(Border.WidthProperty, animation);
        }

        private void WatchtCases_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Visibility = Visibility.Collapsed;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            using (DataDB.entities = new EntitiesMain())
            {
                TransactionType type = (TransactionType)CbType.SelectedItem;
                Transactions transactions = new Transactions()
                {
                    TransactionsName = TbName.Text,
                    Type = type.TransactionsTypeId
                };
                DataDB.entities.Transactions.Add(transactions);
                DataDB.entities.SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            DataFinance();
            FormContainer.Visibility = Visibility.Collapsed;
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            Transactions transactions = (sender as Button).DataContext as Transactions;

            using (DataDB.entities = new EntitiesMain()) 
            {
                DataDB.entities.Entry(transactions).State = EntityState.Deleted;
                DataDB.entities.SaveChanges();
                MessageBox.Show("Транзакция удалена");
            }
            DataFinance();
        }
    }
}
