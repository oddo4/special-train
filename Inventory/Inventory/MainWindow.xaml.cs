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
        ItemPosition item = new ItemPosition();
        public MainWindow()
        {
            InitializeComponent();
            GridBorder();
        }
        private void shape_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            item = new ItemPosition();
            item.ItemMouseDown(sender, canvas, e);

            //writeData.Content = source.Name;
        }
        private void shape_MouseMove(object sender, MouseEventArgs e)
        {
            item.ItemMove(canvas, e);
        }
        private void shape_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            item.ItemMouseUp(inventory, sender, canvas);
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

        private void CreateItem(int H, int W)
        {
            Label label = new Label() { Content = "New Item", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 8 };
            Rectangle rectangle = new Rectangle() { Height = 40 * H, Width = 40 * W, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = Brushes.AliceBlue, Stroke = Brushes.DarkBlue };
            Grid grid = new Grid();
            grid.MouseLeftButtonDown += shape_MouseLeftButtonDown;
            grid.MouseMove += shape_MouseMove;
            grid.MouseLeftButtonUp += shape_MouseLeftButtonUp;
            Canvas.SetLeft(grid, 0);
            Canvas.SetTop(grid, 0);
            grid.Children.Add(rectangle);
            grid.Children.Add(label);
            canvas.Children.Add(grid);
        }

        private void lblCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateItem(int.Parse(tboxHeight.Text), int.Parse(tboxWidth.Text));
        }
    }
}
