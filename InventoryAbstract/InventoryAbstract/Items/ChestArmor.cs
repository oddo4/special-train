﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    class ChestArmor : AArmor, IUseable
    {
        public string Use()
        {
            return "";
        }
    }
}
