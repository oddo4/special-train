using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public abstract class AWeapon : AItemType, IUpgradeable
    {
        //public int Attack { get; set; }

        public string Upgrade()
        {
            return "";
        }
    }
}
