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

namespace sqlwpf
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TodoItemDatabase database = new TodoItemDatabase("filedata - kopie.db3");

        public MainWindow()
        {
            InitializeComponent();

            /*TodoItem car = new TodoItem() { Name = "Nazev1", Power = 10, Manufacture = "Vyrobce5", State = "Ojeté" };
            database.SaveItemAsync(car);*/

            listTest.ItemsSource = database.GetItemsAsync().Result.OrderBy(c => c.Manufacture).ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listTest.SelectedIndex >= 0)
            {
                var list = database.GetItemsAsync().Result.OrderBy(c => c.Manufacture).ToList();
                database.DeleteItemAsync(list[listTest.SelectedIndex]);
                listTest.ItemsSource = null;
                listTest.ItemsSource = database.GetItemsAsync().Result.OrderBy(c => c.Manufacture).ToList();
            }
        }
    }
}
