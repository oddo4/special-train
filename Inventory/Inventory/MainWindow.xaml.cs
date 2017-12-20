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
        double xShape, yShape, xCanvas, yCanvas;
        public MainWindow()
        {
            InitializeComponent();
            GridBorder();
        }
        private void shape_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            begpos = Mouse.GetPosition(canvas);
            source = (FrameworkElement)sender;
            Mouse.Capture(source);
            captured = true;
            xShape = Canvas.GetLeft(source);
            yShape = Canvas.GetTop(source);
            xCanvas = e.GetPosition(canvas).X;
            yCanvas = e.GetPosition(canvas).Y;
            

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
            if (pos.X <= 240 && pos.X >= 0 && pos.Y <= 240 && pos.Y >= 0)
            {
                if (begpos.X + 40 > pos.X && begpos.X - 40 < pos.X)
                {
                    SetPositionGrid((Grid)sender, pos.X, pos.Y);
                }
                else
                {
                    SetPositionGrid((Grid)sender, begpos.X, begpos.Y);
                }
            }
            else
            {
                SetPositionGrid((Grid)sender, begpos.X, begpos.Y);
            }
        }

        private void SetPositionGrid(Grid el, double posX, double posY)
        {
            var ch = el.Children[0];
            var rec = (Rectangle)ch;

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
                    test.Content = x + ", " + y + "; " + posX + ", " + posY;
                    Canvas.SetLeft(el, x * 40);
                    Canvas.SetTop(el, y * 40);
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
                    Border border = new Border();
                    border.BorderBrush = Brushes.DarkGray;
                    border.BorderThickness = new Thickness(1);
                    inventory.Children.Add(border);
                    Grid.SetRow(border, j);
                    Grid.SetColumn(border, i);
                }
            }
        }
    }
}
