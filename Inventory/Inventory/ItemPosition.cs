using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Inventory
{
    class ItemPosition
    {
        public FrameworkElement source = null;
        public double Width = 0;
        public double Height = 0;
        public Point BegPos { get; set; }
        public bool Captured = false;
        public double xShape, yShape, xCanvas, yCanvas;
        public Rectangle Rectangle = null;

        public void ItemMouseDown(object sender, Canvas canvas, MouseEventArgs e)
        {
            var gr = (Grid)sender;
            var ch = gr.Children[0];
            Rectangle = (Rectangle)ch;
            Canvas.SetZIndex(gr, 100);
            BegPos = Mouse.GetPosition(canvas);
            source = (FrameworkElement)sender;
            Canvas.SetLeft(source, BegPos.X + 5);
            Canvas.SetTop(source, BegPos.Y + 5);
            xShape = Canvas.GetLeft(source);
            yShape = Canvas.GetTop(source);
            xCanvas = e.GetPosition(canvas).X;
            yCanvas = e.GetPosition(canvas).Y;
            Rectangle.Opacity = 0.75;
            Mouse.Capture(source);
            Captured = true;
        }

        public void ItemMove(Canvas canvas, MouseEventArgs e)
        {
            if(Captured)
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

        public void ItemMouseUp(Grid inventory, object sender, Canvas canvas)
        {
            Mouse.Capture(null);
            Captured = false;
            var pos = Mouse.GetPosition(canvas);
            if (pos.X <= (inventory.RowDefinitions.Count() * 40) && pos.X >= 0 && pos.Y <= (inventory.ColumnDefinitions.Count() * 40) && pos.Y >= 0)
            {
                SetPositionOnGrid((Grid)sender, inventory, pos.X, pos.Y, Rectangle.ActualWidth, Rectangle.ActualHeight);
            }
            else
            {
                SetPositionOnGrid((Grid)sender, inventory, BegPos.X, BegPos.Y, Rectangle.ActualWidth, Rectangle.ActualHeight);
            }
        }

        public bool SetPositionOnGrid(Grid item, Grid inventory, double posX, double posY, double width, double height)
        {
            for (int y = 0; y < inventory.ColumnDefinitions.Count; y++)
            {
                int x = 0;
                for (int j = 0; j < inventory.RowDefinitions.Count; j++)
                {
                    if (posX > j * 40 && posX < (j + 1) * 40)
                    {
                        if (width / 40 > 1 && j + (width / 40) > inventory.RowDefinitions.Count - 1)
                        {
                            x = inventory.RowDefinitions.Count - ((int)width / 40);
                        }
                        else
                        {
                            x = j;
                        }
                        break;
                    }
                }

                if (posY > y * 40 && posY < (y + 1) * 40)
                {
                    if (height / 40 > 1 && y + (height / 40) > inventory.ColumnDefinitions.Count - 1)
                    {
                        y = inventory.ColumnDefinitions.Count - ((int)height / 40);
                    }

                    Canvas.SetLeft(item, x * 40);
                    Canvas.SetTop(item, y * 40);
                    Rectangle.Opacity = 0.90;
                    Canvas.SetZIndex(item, 0);
                    return true;
                }
            }

            return false;
        }
    }
}
