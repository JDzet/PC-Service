using PC_Service.Pages;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddTransaction.xaml
    /// </summary>
    public partial class AddTransaction : Window
    {
        int _type;
        public AddTransaction(string Text, int Type)
        {
            InitializeComponent();
            TbName.Text = Text;
            _type = Type;
            DataTransaction();
        }

        public void DataTransaction() 
        {
            using (DataDB.entities = new EntitiesMain()) 
            {
                CbOperetion.ItemsSource = DataDB.entities.Transactions.Where(x => x.Type == _type).ToList();
                var clients = DataDB.entities.Client.Select(c => c.ClientName);
                var users = DataDB.entities.User.Select(u => u.UserName);

                var names = clients.Concat(users).ToList();
                CBClient.ItemsSource = names;
            }
        }


        public bool CheckData()
        {

            var fieldsToCheck = new List<Control>
            {
                TBNote,TbPrice,CbOperetion,DatePicekt
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
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!CheckData())
            {
                return;
            }


            try
            {
                decimal coming, expenses;
                if (_type == 1)
                {
                    coming = decimal.Parse(TbPrice.Text);
                    expenses = 0;
                }
                else
                {
                    coming = 0;
                    expenses = decimal.Parse(TbPrice.Text);
                }

                string selectedName = CBClient.SelectedItem as string;
                Client client = new Client();
                User user = new User();

                using (DataDB.entities = new EntitiesMain())
                {
                    if (selectedName != null)
                    {
                        client = DataDB.entities.Client.FirstOrDefault(c => c.ClientName == selectedName);
                        user = DataDB.entities.User.FirstOrDefault(u => u.UserName == selectedName);
                    }

                    var transacClientId = client != null ? client.ClientId : (int?)null;
                    var transacUserId = user != null ? user.UserId : (int?)null;


                    Transactions tr = (Transactions)CbOperetion.SelectedItem;
                    HistoryTransaction transact = new HistoryTransaction()
                    {
                        HistoryNameTransactions = tr.TransactionsID,
                        Coming = coming,
                        Expenses = expenses,
                        Description = TBNote.Text,
                        TransacUser = transacUserId == 0 ? null : transacUserId,
                        TransacClient = transacClientId == 0 ? null: transacClientId,
                        Date = DatePicekt.SelectedDate.GetValueOrDefault(),
                        UserCreate = UserAuthorization.Worker.UserId
                    };

                    if (_type == 1) 
                    {
                        var capital = DataDB.entities.Capital.FirstOrDefault();
                        capital.Amount += coming;
                        DataDB.entities.HistoryTransaction.Add(transact);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Операция сохранена");
                    }
                    else 
                    {
                        var capital = DataDB.entities.Capital.FirstOrDefault();
                        capital.Amount -= expenses;
                        DataDB.entities.HistoryTransaction.Add(transact);
                        DataDB.entities.SaveChanges();
                        MessageBox.Show("Операция сохранена");
                    }
                    this.Close();
                }
            } 
            catch 
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }   
    }
}
