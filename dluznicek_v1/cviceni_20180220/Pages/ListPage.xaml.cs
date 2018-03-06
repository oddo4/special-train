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
    /// Interakční logika pro ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        ItemsList itemsList;
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        public ListPage()
        {
            InitializeComponent();
            txtBlkListName.Text = "Všechny výdaje";
            SwitchListNameElement(0);
        }
        public ListPage(ItemsList itemsList)
        {
            InitializeComponent();
            this.itemsList = itemsList;
            txtListName.Text = itemsList.Name;
            SwitchListNameElement(1);
        }
        private void GetItems()
        {
            items = new ObservableCollection<Item>();
            List<Item> result;
            if (itemsList != null)
            {
                result = App.Database.GetItemsAsync(itemsList.ID);
            }
            else
            {
                result = App.Database.GetItemAsync();
            }
            foreach (Item item in result)
            {
                var trans = App.Database.GetTransaction(item.ID);
                item.FormattedDate = trans.DateTransaction.ToString("dd/MM/yyyy");
                items.Add(item);
            }

            items = new ObservableCollection<Item>(items.OrderBy(c => c.FormattedDate));

            lViewItems.ItemsSource = items;
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                App.Database.DeleteItem(items[lViewItems.SelectedIndex]);
                items.RemoveAt(lViewItems.SelectedIndex);
                GetTotalSum();
            }
        }
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                NavigationService.Navigate(new AddPage(items[lViewItems.SelectedIndex]));
            }
        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage(itemsList.ID));

            GetTotalSum();
        }
        private void GetTotalSum()
        {
            int sum = 0;
            foreach (Item item in items)
            {
                sum += item.Cost;
            }

            txtBlkSum.Text = sum.ToString() + " ,-";
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            GetItems();
            if (items.Any())
            {
                GetTotalSum();
            }
        }

        private void btnSaveListName_Click(object sender, RoutedEventArgs e)
        {
            itemsList.Name = txtListName.Text;

            App.Database.SaveItemsListAsync(itemsList);
        }
        private void SwitchListNameElement(int i)
        {
            switch (i)
            {
                case 0:
                    EditListName.Visibility = Visibility.Hidden;
                    txtBlkListName.Visibility = Visibility.Visible;
                    break;
                case 1:
                    EditListName.Visibility = Visibility.Visible;
                    txtBlkListName.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }
    }
}
