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
        FrameworkElement source = null;
        Point begpos;
        bool captured = false;
        bool place = false;
        double xShape, yShape, xCanvas, yCanvas;
        Rectangle rec = null;
        public MainWindow()
        {
            InitializeComponent();
            GridBorder();
        }
        private void shape_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var gr = (Grid)sender;
            var ch = gr.Children[0];
            rec = (Rectangle)ch;
            Canvas.SetZIndex(gr, 100);
            begpos = Mouse.GetPosition(canvas);
            source = (FrameworkElement)sender;
            Canvas.SetLeft(source, begpos.X + 5);
            Canvas.SetTop(source, begpos.Y + 5);
            xShape = Canvas.GetLeft(source);
            yShape = Canvas.GetTop(source);
            xCanvas = e.GetPosition(canvas).X;
            yCanvas = e.GetPosition(canvas).Y;
            rec.Opacity = 0.75;
            Mouse.Capture(source);
            captured = true;

            //writeData.Content = source.Name;
        }
        private void shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                xShape += x - xCanvas;
                Canvas.SetLeft(source, xShape);
                xCanvas = x;
                yShape += y - yCanvas;
                Canvas.SetTop(source, yShape);
                yCanvas = y;

            }
        }
        private void shape_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
            var pos = Mouse.GetPosition(canvas);
            if (pos.X <= (inventory.RowDefinitions.Count() * 40) && pos.X >= 0 && pos.Y <= (inventory.ColumnDefinitions.Count() * 40) && pos.Y >= 0)
            {
                SetPositionGrid((Grid)sender, pos.X, pos.Y, rec.ActualWidth, rec.ActualHeight);
            }
            else
            {
                SetPositionGrid((Grid)sender, begpos.X, begpos.Y, rec.ActualWidth, rec.ActualHeight);
            }
        }

        private void SetPositionGrid(Grid el, double posX, double posY, double width, double height)
        {
            for (int y = 0; y < inventory.ColumnDefinitions.Count; y++)
            {
                int x = 0;
                for (int j = 0; j < inventory.RowDefinitions.Count; j++)
                {
                    if (posX > j * 40 && posX < (j + 1) * 40)
                    {
                        x = j;
                        break;
                    }
                }

                if (posY > y * 40 && posY < (y + 1) * 40)
                {
                    if (x > (inventory.RowDefinitions.Count / 2)-1)
                    {
                        x = x * (40 * ((int)width / 40) - 1);
                    }
                    if (y > (inventory.ColumnDefinitions.Count / 2) - 1)
                    {
                        y = y * (40 * ((int)width / 40) - 1);
                    }
                    test.Content = x + ", " + y + "; " + begpos.X + ", " + begpos.Y + "; " + posX + ", " + posY;
                    Canvas.SetLeft(el, x);
                    Canvas.SetTop(el, y);
                    rec.Opacity = 1;
                    Canvas.SetZIndex(el, 0);
                    break;
                }
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
    }
}
