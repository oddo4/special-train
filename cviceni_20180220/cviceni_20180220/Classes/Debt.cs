using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_20180220
{
    public class Debt
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDTransaction { get; set; }
        public DateTime NextDateToPay { get; set; }
        public int RaiseCounter { get; set; }
        public double RaisePercentage { get; set; }
    }
}
