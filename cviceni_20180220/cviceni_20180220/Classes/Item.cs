using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_20180220
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        [Ignore]
        public string FormattedDate { get; set; }
    }
}
