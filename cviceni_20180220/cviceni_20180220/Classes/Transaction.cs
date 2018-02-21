using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_20180220
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDItem { get; set; }
        public bool Done { get; set; }
        public DateTime DateSpent { get; set; }
    }
}
