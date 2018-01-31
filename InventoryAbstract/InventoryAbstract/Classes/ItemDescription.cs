using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public class ItemDescription
    {
        public string Name { get; set; }
        public ItemDescription(string name)
        {
            this.Name = name;
        }
    }
}
