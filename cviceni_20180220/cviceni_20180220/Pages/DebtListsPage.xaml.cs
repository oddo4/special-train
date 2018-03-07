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
    /// Interakční logika pro DebtListsPage.xaml
    /// </summary>
    public partial class DebtListsPage : Page
    {
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        ObservableCollection<ItemsList> itemsLists = new ObservableCollection<ItemsList>();
        public DebtListsPage()
        {
            InitializeComponent();
            SetItems();
            SetItemsLists();
        }
        private void SetItems()
        {
            items = new ObservableCollection<Item>();
            List<Item> result;
            result = App.Database.GetItemSync();

            foreach (Item item in result)
            {
                var debt = App.Database.GetDebt(item.ID);
                if (debt != null)
                {
                    item.FormattedDate = debt.DateToPay.ToString("dd/MM/yyyy");
                    items.Add(item);
                }
            }

            lViewItems.ItemsSource = items;
        }
        private void SetItemsLists()
        {
            itemsLists = new ObservableCollection<ItemsList>();
            var result = App.Database.GetItemsLists(1);
            foreach (ItemsList iList in result)
            {
                itemsLists.Add(iList);
            }

            lViewLists.ItemsSource = itemsLists;
        }
        private void btnCreateList_Click(object sender, RoutedEventArgs e)
        {
            ItemsList newItemsList = new ItemsList();
            newItemsList.Name = txtListName.Text;
            newItemsList.Type = 1;

            if (App.Database.SaveItemsListSync(newItemsList) != 0)
            {
                newItemsList = App.Database.GetAllItemsLists().Last();
            }

            NavigateToPage(new ListPage(newItemsList));

            ResetElements();
        }
        private void btnDeleteList_Click(object sender, RoutedEventArgs e)
        {
            if (lViewLists.SelectedIndex != -1)
            {
                App.Database.DeleteItemsList(itemsLists[lViewLists.SelectedIndex]);
                itemsLists.RemoveAt(lViewLists.SelectedIndex);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new AddPage());
        }
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            if (lViewLists.SelectedIndex != -1)
            {
                var itemsList = itemsLists[lViewLists.SelectedIndex];
                NavigateToPage(new ListPage(itemsList));
            }
        }
        private void NavigateToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
        private void ResetElements()
        {
            txtListName.Text = "";
            SetItems();
            SetItemsLists();
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnDeleteDebt_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                App.Database.DeleteItem(items[lViewItems.SelectedIndex]);
                items.RemoveAt(lViewItems.SelectedIndex);
            }
        }

        private void btnSortCostAsc_Click(object sender, RoutedEventArgs e)
        {
            var sorted = items.OrderBy(i => i.Cost).ToList();
            items = new ObservableCollection<Item>();
            foreach (Item item in sorted)
            {
                items.Add(item);
            }
            lViewItems.ItemsSource = items;
        }

        private void btnSortCostDesc_Click(object sender, RoutedEventArgs e)
        {
            var sorted = items.OrderByDescending(i => i.Cost).ToList();
            items = new ObservableCollection<Item>();
            foreach (Item item in sorted)
            {
                items.Add(item);
            }
            lViewItems.ItemsSource = items;
        }

        private void btnSortNotPayed_Click(object sender, RoutedEventArgs e)
        {
            var today = DateTime.Today;
            var allItems = items;
            List<Item> notpayed = new List<Item>();

            items = new ObservableCollection<Item>();
            foreach (Item item in allItems)
            {
                var trans = App.Database.GetDebt(item.ID);
                if (trans.DateToPay < today)
                {
                    items.Add(item);
                }
                else
                {
                    notpayed.Add(item);
                }
            }

            itemsAddRange(notpayed);

            lViewItems.ItemsSource = items;
        }
        private void itemsAddRange(List<Item> list)
        {
            foreach (Item item in list)
            {
                items.Add(item);
            }
        }

        private void btnSortNewest_Click(object sender, RoutedEventArgs e)
        {
            var itemsCol = items;
            var debtsList = App.Database.GetDebtSync().OrderByDescending(i => i.DateToPay).ToList();

            items = new ObservableCollection<Item>();
            for (int i = 0; i < itemsCol.Count; i++)
            {
                var item = items.Where(c => c.ID == debtsList[i].IDItem).First();
                items.Add(item);
            }
            lViewItems.ItemsSource = items;
        }

        private void btnSortOldest_Click(object sender, RoutedEventArgs e)
        {
            var sorted = items.OrderByDescending(i => i.FormattedDate).ToList();
            items = new ObservableCollection<Item>();
            foreach (Item item in sorted)
            {
                items.Add(item);
            }
            lViewItems.ItemsSource = items;
        }
    }
}
