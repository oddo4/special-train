using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlwpf
{
    class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public string Manufacture { get; set; }
        public string State { get; set; }
        public TodoItem()
        {
        }
    }
}
