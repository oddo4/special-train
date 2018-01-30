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
        List<AItem> allItems = new List<AItem>();
        AItem selectedItem = null;
        public MainWindow()
        {
            InitializeComponent();
            AItem item = new Helm("Helma");
            allItems.Add(item);
            CreateItem(item);
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
        private void CreateItem(AItem item)
        {
            Label label = new Label() { Content = item.Name, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 8 };
            Label count = new Label() { Content = item.Count, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, FontSize = 6 };
            Rectangle rectangle = new Rectangle() { Height = 40, Width = 40, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = Brushes.OrangeRed, Stroke = Brushes.DarkRed };
            Grid grid = new Grid();
            grid.Children.Add(rectangle);
            grid.Children.Add(label);
            grid.Children.Add(count);
            grid.MouseLeftButtonDown += selectItem;
            Grid.SetRow(grid, (int)item.Pos.X);
            Grid.SetColumn(grid, (int)item.Pos.Y);
            inventory.Children.Add(grid);
        }
        private void selectItem(object sender, MouseEventArgs e)
        {
            var grid = (Grid)sender;
            Point pos = new Point(Grid.GetRow(grid), Grid.GetColumn(grid));
            foreach (AItem item in allItems)
            {
                if (pos == item.Pos)
                {
                    selectedItem = item;
                }
            }


        }
        private void showInfo()
        {
            
        }
    }
}
