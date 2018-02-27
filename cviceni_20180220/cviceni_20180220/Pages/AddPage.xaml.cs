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
        int idItemsList;
        Item item;
        Transaction transaction;
        public AddPage()
        {
            InitializeComponent();
            item = new Item();
            transaction = new Transaction();
        }
        public AddPage(int id)
        {
            InitializeComponent();
            idItemsList = id;
            item = new Item();
            transaction = new Transaction();
        }
        public AddPage(Item item)
        {
            InitializeComponent();
            this.item = item;
            transaction = App.Database.GetTransaction(item.ID);
            txtName.Text = item.Name;
            txtCost.Text = item.Cost.ToString();
            dpDate.SelectedDate = transaction.DateTransaction;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = item;
            newItem.Name = txtName.Text;
            newItem.Cost = int.Parse(txtCost.Text);
            App.Database.SaveItemAsync(newItem);

            newItem = App.Database.GetItemAsync().Last();
            Transaction newTransaction = transaction;
            newTransaction.IDItem = newItem.ID;
            newTransaction.DateTransaction = dpDate.SelectedDate.Value;

            App.Database.SaveTransactionAsync(newTransaction);

            ItemTies newItemTies = new ItemTies();
            newItemTies.IDItem = newItem.ID;
            newItemTies.IDItemsList = idItemsList;

            App.Database.SaveItemTiesAsync(newItemTies);

            NavigationService.GoBack(); ;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
