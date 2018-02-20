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
        }
        public ListPage(ItemsList itemsList)
        {
            InitializeComponent();
            this.itemsList = itemsList;
            GetItems();
        }
        private void GetItems()
        {
            items = new ObservableCollection<Item>();
            var result = App.Database.GetItemsAsync(itemsList.ID);
            foreach (Item item in result)
            {
                items.Add(item);
            }
            lViewItems.ItemsSource = items;
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnDelItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = new Item();
            newItem.Name = txtName.Text;
            newItem.Cost = int.Parse(txtCost.Text);
            newItem.DateSpent = dpDate.SelectedDate.Value;

            var idItem = App.Database.SaveItemAsync(newItem).Result;

            newItem.ID = idItem;

            ItemTies newItemTies = new ItemTies();
            newItemTies.IDItem = newItem.ID;
            newItemTies.IDItemsList = itemsList.ID;

            App.Database.SaveItemTiesAsync(newItemTies);

            items.Add(newItem);
        }
    }
}
