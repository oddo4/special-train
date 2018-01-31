using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public interface IStackable
    {
        int Quantity { get; set; }
        int MaxQuantity { get; set; }
        bool Count();
    }
}
