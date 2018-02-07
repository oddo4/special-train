using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180207
{
    public class Znamka
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Hodnota { get; set; }
        public int IDStudent { get; set; }
        public int IDPredmet { get; set; }
        [Ignore]
        public string NazevPredmetu
        {
            get
            {
                return App.Databaze.GetPredmetName(IDPredmet).Result.Nazev;
            }
        }

    }
}
