using System;
using System.Collections.Generic;
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

namespace InventoryAbstract
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Item> allItems = new List<Item>();
        Item selectedItem = null;
        public MainWindow()
        {
            InitializeComponent();
            GridBorder();
            Item item = new Item();
            item.ItemType = new Helm();
            item.ItemDescription = new ItemDescription("Helm");
            item.ItemPosition = new ItemPosition(new Point(0, 0), new ItemSize(1, 1));

            Item item2 = new Item();
            item2.ItemType = new ShortRange();
            item2.ItemDescription = new ItemDescription("Sword");
            item2.ItemPosition = new ItemPosition(new Point(1, 0), new ItemSize(1, 1));

            Item item3 = new Item();
            item3.ItemType = new Food();
            item3.ItemDescription = new ItemDescription("Cheese");
            item3.ItemPosition = new ItemPosition(new Point(2, 0), new ItemSize(1, 1));

            ((Food)item3.ItemType).MaxQuantity = 10;
            ((Food)item3.ItemType).Quantity = 5;

            allItems.Add(item);
            allItems.Add(item2);
            allItems.Add(item3);
            foreach (Item c in allItems)
            {
                CreateItem(c);
            }
        }
        private void GridBorder()
        {
            for (int i = 0; i < inventory.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < inventory.RowDefinitions.Count; j++)
                {
                    Rectangle border = new Rectangle();
                    border.Stroke = Brushes.DarkGray;
                    border.StrokeThickness = 1;
                    border.Fill = Brushes.AliceBlue;
                    inventory.Children.Add(border);
                    Grid.SetRow(border, j);
                    Grid.SetColumn(border, i);
                }
            }
        }
        private void CreateItem(Item item)
        {
            Grid grid = new Grid();
            Rectangle rectangle = new Rectangle() { Height = (item.ItemPosition.Size.H * 60), Width = (item.ItemPosition.Size.W * 60), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = Brushes.OrangeRed, Stroke = Brushes.DarkRed };
            grid.Children.Add(rectangle);
            Label label = new Label() { Content = item.ItemDescription.Name, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 12 };
            grid.Children.Add(label);
            if (item.ItemType is AConsumable)
            {
                Label count = new Label() { Content = ((Food)item.ItemType).Quantity, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, FontSize = 15 };
                grid.Children.Add(count);
            }
            grid.MouseLeftButtonDown += selectItem;
            Grid.SetRow(grid, (int)item.ItemPosition.Pos.X);
            Grid.SetColumn(grid, (int)item.ItemPosition.Pos.Y);
            inventory.Children.Add(grid);
        }
        private void selectItem(object sender, MouseEventArgs e)
        {
            var grid = (Grid)sender;
            Point pos = new Point(Grid.GetRow(grid), Grid.GetColumn(grid));
            foreach (Item item in allItems)
            {
                if (pos == item.ItemPosition.Pos)
                {
                    selectedItem = item;
                }
            }

            info.Content = "Selected: " + selectedItem.ItemDescription.Name;
        }
        private void showInfo()
        {
            
        }

        private void addStack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
