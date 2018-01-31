using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryAbstract
{
    public class ItemPosition
    {
        public Point Pos { get; set; }
        public ItemSize Size { get; set; }
        public ItemPosition(Point pos, ItemSize size)
        {
            this.Pos = pos;
            this.Size = size;
        }
        public void ChangePos(Point pos)
        {
            this.Pos = pos;
        }
    }
}
