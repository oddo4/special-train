using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            //App.Database.DeleteTables();
            CheckDebts();
            SetTotalYear();
            SetTotalMonth();
        }
        private void SetTotalYear()
        {
            var result = App.Database.GetItemTotalYear();
            var totalYear = 0.0;
            if (result != null)
            {
                foreach (Item item in result)
                {
                    totalYear += item.Cost;
                }
            }
            txtBlkTotalYear.Text = "Tento rok: " + totalYear.ToString() + " ,-";
        }
        private void SetTotalMonth()
        {
            var result = App.Database.GetItemTotalMonth();
            var totalMonth = 0.0;
            if (result != null)
            {
                foreach (Item item in result)
                {
                    totalMonth += item.Cost;
                }
            }

            txtBlkTotalMonth.Text = "Tento měsíc: " + totalMonth.ToString() + " ,-";
        }
        private void NavigateToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
        /*private void UpdatePage(object sender, EventArgs e)
        {
            SetTotalYear();
            SetTotalMonth();
        }*/
        private void CheckDebts()
        {
            var today = DateTime.Today;
            var result = App.Database.GetItemSync();

            foreach (Item item in result)
            {
                var debt = App.Database.GetDebt(item.ID);
                if (debt != null)
                {
                    if (debt.DateToPay < today)
                    {
                        item.Cost += (debt.RaisePercentage / 100) * item.Cost;

                        App.Database.SaveItemSync(item);
                    }
                }
            }
        }
        private void btnShowSpendLists_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new SpendListsPage());
        }

        private void btnShowDebtLists_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new DebtListsPage());
        }
    }
}
