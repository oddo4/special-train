using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryAbstract
{
    public class Item
    {
        public ItemDescription ItemDescription { get; set; }
        public ItemPosition ItemPosition { get; set; }
        public AItemType ItemType { get; set; }
    }
}
