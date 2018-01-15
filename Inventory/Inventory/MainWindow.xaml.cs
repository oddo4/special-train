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

namespace Inventory
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemPosition itemPosition = new ItemPosition();
        List<ItemData> itemsList = new List<ItemData>();
        ItemDatabase db = new ItemDatabase(@"InventoryFile.db3");
        List<List<int>> gridList = new List<List<int>>();
        public MainWindow()
        {
            InitializeComponent();
            GridBorder();
            CreateArray();
            LoadDb();
        }
        private void shape_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            itemPosition = new ItemPosition();
            foreach (ItemData item in itemsList)
            {
                if (item.XSize <= Math.Floor(Mouse.GetPosition(canvas).X / 30) && item.XSize + (item.Width / 30) > Math.Floor(Mouse.GetPosition(canvas).X / 30) && item.YSize <= Math.Floor(Mouse.GetPosition(canvas).Y / 30) && item.YSize + (item.Height / 30) > Math.Floor(Mouse.GetPosition(canvas).Y / 30))
                {
                    itemPosition.itemData = item;
                }
            }
            itemPosition.ItemMouseDown(sender, canvas, inventory, e, gridList);
            gridTest();
            test2.Content = itemPosition.BegPos.X + ", " + itemPosition.BegPos.Y;
            //writeData.Content = source.Name;
        }
        private void shape_MouseMove(object sender, MouseEventArgs e)
        {
            itemPosition.ItemMove(canvas, e);
        }
        private void shape_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            itemPosition.ItemMouseUp(sender, canvas, inventory, gridList);
            gridTest();
        }
        private void gridTest()
        {
            string gridString = "";
            for (int i = 0; i < gridList.Count; i++)
            {
                for (int j = 0; j < gridList[0].Count; j++)
                {
                    gridString += gridList[j][i];
                }
                gridString += Environment.NewLine;
            }

            test.Content = gridString;
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

        private void CreateItem(int H, int W, string name = "Item", int posX = 0, int posY = 0)
        {
            Label label = new Label() { Content = name, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 6 };
            Rectangle rectangle = new Rectangle() { Height = 30 * H, Width = 30 * W, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = Brushes.OrangeRed, Stroke = Brushes.DarkRed };
            Grid grid = new Grid();
            grid.MouseLeftButtonDown += shape_MouseLeftButtonDown;
            grid.MouseMove += shape_MouseMove;
            grid.MouseLeftButtonUp += shape_MouseLeftButtonUp;
            Canvas.SetLeft(grid, 0);
            Canvas.SetTop(grid, 0);
            grid.Children.Add(rectangle);
            grid.Children.Add(label);
            canvas.Children.Add(grid);
            ItemData newItem = new ItemData(name, posX, posY, H * 30, W * 30);
            itemsList.Add(newItem);
            itemPosition.rect = rectangle;
            itemPosition.SetPositionOnGrid(grid, inventory, newItem, gridList, posX * 30, posY * 30, W * 30, H * 30);
            itemPosition.changeGrid(newItem.XSize, newItem.YSize, 1, gridList, newItem.Width, newItem.Height);
            gridTest();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateItem(int.Parse(tboxHeight.Text), int.Parse(tboxWidth.Text), tboxName.Text.ToString());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemData item in itemsList)
            {
                db.SaveItemAsync(item);
            }
        }

        private void CreateArray()
        {
            for (int i = 0; i < inventory.RowDefinitions.Count; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < inventory.ColumnDefinitions.Count; j++)
                {
                    row.Add(0);
                }
                gridList.Add(row);
            }
        }
        private void LoadDb()
        {
            var itemsFromDb = db.GetItemsAsync().Result;
            foreach (ItemData item in itemsFromDb)
            {
                CreateItem(item.Height / 30, item.Width / 30, item.Name, item.XSize, item.YSize);
                for (int i = item.XSize; i < item.XSize + (item.Width / 30); i++)
                {
                    for (int j = item.YSize; j < item.YSize + (item.Height / 30); j++)
                    {
                        gridList[i][j] = 1;
                    }
                }
                itemsList.Add(item);
            }
        }
    }
}
