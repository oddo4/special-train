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
        List<string> manufactureList = new List<string>() { "Škoda", "Audi", "BMW" };
        List<TodoItem> carsList = new List<TodoItem>();

        public MainWindow()
        {
            InitializeComponent();
            CreateManufactureList();

            /*TodoItem car = new TodoItem() { Name = "Nazev1", Power = 10, Manufacture = "BMW", State = "Nové" };
            database.SaveItemAsync(car);*/

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listCars.SelectedIndex >= 0)
            {
                carsList.RemoveAt(listCars.SelectedIndex);
                database.DeleteItemAsync((TodoItem)listCars.SelectedItem);
                listCars.ItemsSource = null;
                CreateCarsList();
            }
        }

        private void Manufacture_Selected(object sender, SelectionChangedEventArgs e)
        {
            CreateCarsList();
        }

        private void Car_Selected(object sender, SelectionChangedEventArgs e)
        {
            ShowCarInfo();
        }

        private void ShowCarInfo()
        {
            if (listCars.SelectedItem != null)
            {
                TodoItem car = (TodoItem)listCars.SelectedItem;
                labelManufacture.Content = car.Manufacture;
                labelName.Content = car.Name;
                //labelState.Content = car.State;
                labelPower.Content = car.Power;
            }
            else
            {
                labelManufacture.Content = null;
                labelName.Content = null;
                //labelState.Content = null;
                labelPower.Content = null;
            }
        }

        private void CreateCarsList()
        {
            carsList = database.GetItemsManufactureAsync(listManufactures.SelectedItem.ToString()).Result;
            listCars.ItemsSource = carsList;
        }

        private void CreateManufactureList()
        {
            manufactureList = manufactureList.OrderBy(c => c).ToList();

            listManufactures.ItemsSource = manufactureList;
        }

    }
}
