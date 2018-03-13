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
    /// Interakční logika pro SpendListsPage.xaml
    /// </summary>
    public partial class AllSpendsPage : Page
    {
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public AllSpendsPage()
        {
            InitializeComponent();
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            ResetElements();
        }
        private void SetItems()
        {
            items = new ObservableCollection<Item>();
            List<Item> result;
            result = App.Database.GetItemSync();

            foreach (Item item in result)
            {
                var trans = App.Database.GetTransaction(item.ID);
                var tie = App.Database.GetItemTiesSync().Where(i => i.IDItem == item.ID).First();
                var itemListName = App.Database.GetItemsList(tie.IDItemsList);

                if (trans != null)
                {
                    item.ListName = itemListName.Name;
                    item.FormattedDate = trans.DateTransaction.ToString("dd/MM/yyyy");
                    items.Add(item);
                }
            }

            lViewItems.ItemsSource = items;
        }
        private void NavigateToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
        private void ResetElements()
        {
            SetItems();
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnShowSpendLists_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new SpendListsPage());
        }
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (lViewItems.SelectedIndex != -1)
            {
                App.Database.DeleteItem(items[lViewItems.SelectedIndex]);
                items.RemoveAt(lViewItems.SelectedIndex);
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
