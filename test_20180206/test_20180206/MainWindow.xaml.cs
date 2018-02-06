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

namespace test_20180206
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database database = new Database("FileData.db3");
        ObservableCollection<Item> listDefinedItems = new ObservableCollection<Item>();
        ObservableCollection<Category> listCategories = new ObservableCollection<Category>();
        ObservableCollection<Item> listItems = new ObservableCollection<Item>();
        public MainWindow()
        {
            InitializeComponent();
            /*ItemTies tie1 = new ItemTies();
            tie1.IDCategory = 0;
            tie1.IDItem = 2;
            database.SaveItemTiesAsync(tie1);*/

            CreateDefinedItemsCollection();
            CreateCategoriesCollection();
            listViewCategories.ItemsSource = listCategories;
            cBoxItems.ItemsSource = listDefinedItems;
        }
        private void Categories_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (listViewCategories.SelectedIndex != -1)
            {
                CreateItemsCollection(listCategories[listViewCategories.SelectedIndex]);
                listViewItems.ItemsSource = listItems;
                SwitchElements(1);
            }
        }
        private void Items_Selected(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void CreateDefinedItemsCollection()
        {
            listDefinedItems = new ObservableCollection<Item>();
            var result = database.GetDefinedItemsAsync().Result;

            foreach (Item item in result)
            {
                listDefinedItems.Add(item);
            }
        }
        private void CreateCategoriesCollection()
        {
            listCategories = new ObservableCollection<Category>();
            var result = database.GetCategoriesAsync().Result;

            foreach (Category category in result)
            {
                listCategories.Add(category);
            }
        }
        private void CreateItemsCollection(Category cat)
        {
            listItems = new ObservableCollection<Item>();
            var result = database.GetItemsAsync(cat.ID).Result;

            foreach (ItemTies item in result)
            {
                listItems.Add(listDefinedItems.Where(i => i.ID == item.IDItem).First());
            }
        }
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            listItems.Add((Item)cBoxItems.SelectionBoxItem);
            ItemTies newTie = new ItemTies();
            newTie.IDCategory = listCategories[listViewCategories.SelectedIndex].ID;
            newTie.IDItem = ((Item)cBoxItems.SelectionBoxItem).ID;
            database.SaveItemTiesAsync(newTie);
        }
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            database.DeleteItemAsync(listCategories[listViewCategories.SelectedIndex], listItems[listViewItems.SelectedIndex]);
            listItems.Remove(listItems[listViewItems.SelectedIndex]);
        }

        private void SwitchElements(int i)
        {
            switch (i)
            {
                case 0:
                    cBoxItems.IsEnabled = false;
                    btnAddItem.IsEnabled = false;
                    break;
                case 1:
                    cBoxItems.IsEnabled = true;
                    btnAddItem.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private void btnAddCat_Click(object sender, RoutedEventArgs e)
        {
            Category newCat = new Category();
            newCat.Name = txtAddCat.Text;
            listCategories.Add(newCat);
            database.SaveCategoryAsync(newCat);
            txtAddCat.Text = "";
        }

        private void btnDeleteCat_Click(object sender, RoutedEventArgs e)
        {
            database.DeleteCategoryAsync(listCategories[listViewCategories.SelectedIndex]);
            listCategories.Remove(listCategories[listViewCategories.SelectedIndex]);
            listViewItems.ItemsSource = null;
            listViewCategories.SelectedItem = null;
            SwitchElements(0);
        }
    }
}
