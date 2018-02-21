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
        ObservableCollection<ItemsList> itemsLists = new ObservableCollection<ItemsList>();
        public MainPage()
        {
            InitializeComponent();
            //App.Database.DeleteTables();
            GetItemsLists();
        }
        private void GetItemsLists()
        {
            itemsLists = new ObservableCollection<ItemsList>();
            var result = App.Database.GetItemsListsAsync().Result;
            foreach (ItemsList iList in result)
            {
                itemsLists.Add(iList);
            }

            lViewLists.ItemsSource = itemsLists;
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
        private void btnCreateList_Click(object sender, RoutedEventArgs e)
        {
            ItemsList newItemsList = new ItemsList();
            newItemsList.Name = txtListName.Text;
            
            if (App.Database.SaveItemsListAsync(newItemsList).Result != 0)
            {
                newItemsList = App.Database.GetItemsListsAsync().Result.Last();
            }    

            NavigateToPage(new ListPage(newItemsList));

            ResetElements();
            GetItemsLists();
        }
        private void NavigateToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
        private void ResetElements()
        {
            txtListName.Text = "";
        }
    }
}
