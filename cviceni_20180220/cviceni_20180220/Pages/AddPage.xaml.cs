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
            transaction = new Transaction();

            if (itemsList.Type == 0)
            {
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
            transaction = App.Database.GetTransaction(item.ID);

            txtName.Text = item.Name;
            txtCost.Text = item.Cost.ToString();
            dpDate.SelectedDate = transaction.DateTransaction;

            if (itemsList.Type == 1)
            {
                ShowRaiseField();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = item;
            newItem.Name = txtName.Text;
            newItem.Cost = int.Parse(txtCost.Text);
            App.Database.SaveItemSync(newItem);

            newItem = App.Database.GetAllItemsSync().Last();

            Transaction newTransaction = transaction;
            newTransaction.IDItem = newItem.ID;
            newTransaction.DateTransaction = dpDate.SelectedDate.Value;

            App.Database.SaveTransactionSync(newTransaction);

            if (itemsList.Type == 1)
            {
                Debt newDebt = debt;
                newDebt.IDTransaction = newTransaction.ID;
                newDebt.NextDateToPay = newTransaction.DateTransaction;
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

            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
        private void ShowRaiseField()
        {
            stackRaise.Visibility = Visibility.Visible;
        }
    }
}
