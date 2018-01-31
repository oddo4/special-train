﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public abstract class AArmor : AItemType
    {
        public int Defense { get; set; }
        public abstract string Upgrade();
    }
}
