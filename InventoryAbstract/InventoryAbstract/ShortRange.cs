using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    class ShortRange : AWeapon
    {
        public ShortRange(string name, int x = 0, int y = 0)
        {
            this.Name = name;
            this.Pos = new System.Windows.Point(x, y);
        }
        public override string Use()
        {
            return "Equiped weapon: " + Name;
        }
        public override string Upgrade()
        {
            Attack += 1;
            return Name + " attack raised to " + Attack;
        }
    }
}
