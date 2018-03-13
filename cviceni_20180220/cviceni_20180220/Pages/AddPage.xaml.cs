using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cviceni_20180220
{
    /// <summary>
    /// Interakční logika pro AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        ItemsList itemsList;
        Item item;
        Transaction transaction;
        Debt debt;

        public AddPage()
        {
            InitializeComponent();
            item = new Item();
            transaction = new Transaction();
        }
        public AddPage(ItemsList itemsList)
        {
            InitializeComponent();
            this.itemsList = itemsList;
            item = new Item();

            if (itemsList.Type == 0)
            {
                transaction = new Transaction();
                txtBlkHeader.Text = "Přidat výdaj";
            }
            else
            {
                debt = new Debt();
                txtBlkHeader.Text = "Přidat dluh";
                ShowRaiseField();
            }
        }
        public AddPage(ItemsList itemsList, Item item)
        {
            InitializeComponent();
            this.item = item;
            txtName.Text = item.Name;
            txtCost.Text = item.Cost.ToString();

            if (itemsList.Type == 0)
            {
                transaction = App.Database.GetTransaction(item.ID);
                dpDate.SelectedDate = transaction.DateTransaction;
            }
            else
            {
                debt = App.Database.GetDebt(item.ID);
                dpDate.SelectedDate = debt.DateToPay;
                ShowRaiseField();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = item;
            newItem.Name = txtName.Text;
            newItem.Cost = int.Parse(txtCost.Text);
            App.Database.SaveItemSync(newItem);

            newItem = App.Database.GetItemSync().Last();

            if (itemsList.Type == 0)
            {
                Transaction newTransaction = transaction;
                newTransaction.IDItem = newItem.ID;
                newTransaction.DateTransaction = dpDate.SelectedDate.Value;

                App.Database.SaveTransactionSync(newTransaction);
            }
            else
            {
                Debt newDebt = debt;
                newDebt.IDItem = newItem.ID;
                newDebt.DateToPay = dpDate.SelectedDate.Value;
                newDebt.NextDateToPay = newDebt.DateToPay;
                newDebt.RaiseCounter = 0;
                newDebt.RaisePercentage = int.Parse(txtRaise.Text);

                App.Database.SaveDebtSync(newDebt);
            }

            if(itemsList.ID != 0)
            {
                ItemTies newItemTies = new ItemTies();
                newItemTies.IDItem = newItem.ID;
                newItemTies.IDItemsList = itemsList.ID;

                App.Database.SaveItemTiesSync(newItemTies);
            }

            NavigationService.GoBack(); ;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ShowRaiseField()
        {
            stackRaise.Visibility = Visibility.Visible;
        }
    }
}
