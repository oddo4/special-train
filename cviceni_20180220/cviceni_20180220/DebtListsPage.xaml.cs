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
            result = App.Database.GetItemAsync();

            foreach (Item item in result)
            {
                var trans = App.Database.GetDebt(item.ID);
                item.FormattedDate = trans.DateToPay.ToString("dd/MM/yyyy");
                items.Add(item);
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

            if (App.Database.SaveItemsListAsync(newItemsList) != 0)
            {
                newItemsList = App.Database.GetAllItemsLists().Last();
            }

            NavigateToPage(new ListPage(newItemsList));

            ResetElements();
            SetItemsLists();
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
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
