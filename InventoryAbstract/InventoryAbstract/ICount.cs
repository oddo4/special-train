﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAbstract
{
    public interface ICount
    {
        int CurrentCount { get; set; }
        int MaxCount { get; set; }
    }
}
