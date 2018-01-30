using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryAbstract
{
    public abstract class AItem
    {
        public string Name { get; set; }
        public Point Pos { get; set; }
    }
}
