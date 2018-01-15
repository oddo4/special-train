using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    class ItemData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XSize { get; set; }
        public int YSize { get; set; }
        public ItemData()
        {

        }
        public ItemData(string name, int x, int y, int h, int w)
        {
            this.Name = name;
            this.XSize = x;
            this.YSize = y;
            this.Width = w;
            this.Height = h;
        }
    }
}
