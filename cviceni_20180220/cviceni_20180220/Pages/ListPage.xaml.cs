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
        public ListPage(ItemsList itemsList)
        {
            InitializeComponent();
            this.itemsList = itemsList;
            txtListName.Text = itemsList.Name;
            SwitchListNameElement(1);
            SetGridViewColumns(itemsList.Type);
        }
        private void SetItems()
        {
            items = new ObservableCollection<Item>();
            List<Item> result;

            result = App.Database.GetItemsSync(itemsList.ID);

            foreach (Item item in result)
            {
                var trans = App.Database.GetTransaction(item.ID);
                var debt = App.Database.GetDebt(trans.ID);

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
                SetTotalSum();
            }
        }
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                NavigationService.Navigate(new AddPage(itemsList, items[lViewItems.SelectedIndex]));
            }
        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage(itemsList));

            SetTotalSum();
        }
        private void SetTotalSum()
        {
            var sum = 0.0;
            foreach (Item item in items)
            {
                sum += item.Cost;
            }

            txtBlkSum.Text = sum.ToString() + " ,-";
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            SetItems();
            if (items.Any())
            {
                SetTotalSum();
            }
        }

        private void btnSaveListName_Click(object sender, RoutedEventArgs e)
        {
            itemsList.Name = txtListName.Text;

            App.Database.SaveItemsListSync(itemsList);
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
        private void SetGridViewColumns(int type)
        {
            if (type == 1)
            {
                colDate.Header = "Datum lhůty splacení";
            }
        }
    }
}
