using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180207
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Jmeno { get; set; }
        public int IDTrida { get; set; }
        [Ignore]
        public string NazevTridy
        {
            get
            {
                return App.Databaze.GetTridaName(IDTrida).Result.Nazev;
            }
            set
            {

            }
        }


    }
}
