using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public abstract class AConsumable : AItemType, IStackable
    {
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public bool Count()
        {
            return false;
        }
    }
}
