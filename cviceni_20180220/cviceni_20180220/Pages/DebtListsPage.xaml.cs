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

namespace cviceni_20180220.Pages
{
    /// <summary>
    /// Interakční logika pro DebtListsPage.xaml
    /// </summary>
    public partial class DebtListsPage : Page
    {
        ObservableCollection<ItemsList> itemsLists = new ObservableCollection<ItemsList>();
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public DebtListsPage()
        {
            InitializeComponent();
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            ResetElements();
        }
        private void ResetElements()
        {
            txtListName.Text = "";
            SetItemsLists();
        }
        private void SetItemsLists()
        {
            itemsLists = App.LoadDatasInListView.GetItemsLists(1);
            lViewLists.ItemsSource = itemsLists;
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
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

            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new ListPage(newItemsList));

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
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            if (lViewLists.SelectedIndex != -1)
            {
                var itemsList = itemsLists[lViewLists.SelectedIndex];
                NavigationServiceSingleton.GetNavigationService().NavigateToPage(new ListPage(itemsList));
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
            ICollectionView dataView = CollectionViewSource.GetDefaultView(lViewLists.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
