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
        public ItemData itemData = new ItemData();
        public FrameworkElement source = null;
        public Point BegPos { get; set; }
        public bool Captured = false;
        public double XShape, YShape, XCanvas, YCanvas;
        public Rectangle rect;

        public void ItemMouseDown(object sender, Canvas canvas, Grid inventory, MouseEventArgs e, List<List<int>> gridList)
        {
            var gr = (Grid)sender;
            var ch = gr.Children[0];
            rect = (Rectangle)ch;
            itemData.Width = (int)rect.ActualWidth;
            itemData.Height = (int)rect.ActualHeight;
            Canvas.SetZIndex(gr, 100);
            BegPos = Mouse.GetPosition(canvas);
            source = (FrameworkElement)sender;
            Canvas.SetLeft(source, BegPos.X - 10);
            Canvas.SetTop(source, BegPos.Y - 10);
            XShape = Canvas.GetLeft(source);
            YShape = Canvas.GetTop(source);
            XCanvas = e.GetPosition(canvas).X;
            YCanvas = e.GetPosition(canvas).Y;
            rect.Opacity = 0.60;
            Mouse.Capture(source);
            changeGrid(itemData.XSize, itemData.YSize, 0, gridList, itemData.Width, itemData.Height);
            Captured = true;
        }

        public void ItemMove(Canvas canvas, MouseEventArgs e)
        {
            if(Captured)
            {
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                XShape += x - XCanvas;
                Canvas.SetLeft(source, XShape);
                XCanvas = x;
                YShape += y - YCanvas;
                Canvas.SetTop(source, YShape);
                YCanvas = y;
            }
        }

        public void ItemMouseUp(object sender, Canvas canvas, Grid inventory, List<List<int>> gridList)
        {
            Mouse.Capture(null);
            Captured = false;
            var pos = Mouse.GetPosition(canvas);
            if (pos.X <= (inventory.RowDefinitions.Count() * 30) && pos.X >= 0 && pos.Y <= (inventory.ColumnDefinitions.Count() * 30) && pos.Y >= 0)
            {
                SetPositionOnGrid((Grid)sender, inventory, itemData, gridList, pos.X, pos.Y, itemData.Width, itemData.Height);
            }
            else
            {
                SetPositionOnGrid((Grid)sender, inventory, itemData, gridList, BegPos.X, BegPos.Y, itemData.Width, itemData.Height);
            }
        }

        public bool SetPositionOnGrid(Grid item, Grid inventory, ItemData itemData, List<List<int>> gridList, double posX, double posY, double width, double height)
        {
            for (int y = 0; y < inventory.ColumnDefinitions.Count; y++)
            {
                int x = 0;
                for (int j = 0; j < inventory.RowDefinitions.Count; j++)
                {
                    if (posX >= j * 30 && posX < (j + 1) * 30)
                    {
                        if (width / 30 > 1 && j + (width / 30) > inventory.RowDefinitions.Count - 1)
                        {
                            x = inventory.RowDefinitions.Count - ((int)width / 30);
                        }
                        else
                        {
                            x = j;
                        }
                        break;
                    }
                }

                if (posY >= y * 30 && posY < (y + 1) * 30)
                {
                    if (height / 30 > 1 && y + (height / 30) > inventory.ColumnDefinitions.Count - 1)
                    {
                        y = inventory.ColumnDefinitions.Count - ((int)height / 30);
                    }
                    checkGrid(x, y, gridList, item);
                    rect.Opacity = 1;
                    return true;
                }
            }

            return false;
        }

        private bool checkGrid(int x, int y, List<List<int>> gridList, Grid item)
        {
            for (int i = x; i < x + (itemData.Width / 30); i++)
            {
                for (int j = y; j < y + (itemData.Height / 30); j++)
                {
                    if (gridList[i][j] == 1)
                    {
                        Canvas.SetLeft(item, itemData.XSize * 30);
                        Canvas.SetTop(item, itemData.YSize * 30);
                        changeGrid(itemData.XSize, itemData.YSize, 1, gridList, itemData.Width, itemData.Height);
                        return true;
                    }
                }
            }

            Canvas.SetLeft(item, x * 30);
            Canvas.SetTop(item, y * 30);
            itemData.XSize = x;
            itemData.YSize = y;
            changeGrid(x, y, 1, gridList, itemData.Width, itemData.Height);
            return false;
        }

        public void changeGrid(int posX, int posY, int value, List<List<int>> gridList, int width, int height)
        {
            for (int i = posX; i < posX + (width / 30); i++)
            {
                for (int j = posY; j < posY + (height / 30); j++)
                {
                    gridList[i][j] = value;
                }
            }
        }
    }
}
