using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    class ChestArmor : AArmor
    {
        public ChestArmor(string name, int x = 0, int y = 0)
        {
            this.Name = name;
            this.Pos = new System.Windows.Point(x, y);
        }
        public override string Use()
        {
            return "Equiped chest armor: " + Name;
        }
        public override string Upgrade()
        {
            Defense += 1;
            return Name + " defense raised to " + Defense;
        }
    }
}
