using cviceni_20180220.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class AllDebtsPage : Page
    {
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public AllDebtsPage()
        {
            InitializeComponent();
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            ResetElements();
        }
        private void SetItems()
        {
            items = App.LoadDatasInListView.GetItems(1);
            lViewItems.ItemsSource = items;
        }
        private void ResetElements()
        {
            SetItems();
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }

        private void btnDeleteDebt_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                App.Database.DeleteItem(items[lViewItems.SelectedIndex]);
                items.RemoveAt(lViewItems.SelectedIndex);
            }
        }
        private void btnShowDebtLists_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new DebtListsPage());
        }
        private void btnSortNotPayed_Click(object sender, RoutedEventArgs e)
        {
            var today = DateTime.Today;
            var allItems = items;
            List<Item> notpayed = new List<Item>();

            items = new ObservableCollection<Item>();
            foreach (Item item in allItems)
            {
                var trans = App.Database.GetTransaction(item.ID);
                var debt = App.Database.GetDebt(trans.ID);

                if (debt.NextDateToPay < today)
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
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(lViewItems.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
