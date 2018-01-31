using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public class ItemSize
    {
        public int W { get; set; }
        public int H { get; set; }
        public ItemSize(int width = 1, int height = 1)
        {
            this.W = width;
            this.H = height;
        }
    }
}
