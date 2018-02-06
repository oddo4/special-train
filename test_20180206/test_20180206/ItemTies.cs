using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180206
{
    class ItemTies
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDCategory { get; set; }
        public int IDItem { get; set; }
        
    }
}
