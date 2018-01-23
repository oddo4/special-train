using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_23012018
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Director { get; set; }
        public int Rating { get; set; }

        public Movie()
        {

        }
        public Movie( int Length, int Rating, string Name = "N/A", string Director = "N/A")
        {
            this.Name = Name;
            this.Length = Length;
            this.Director = Director;
            this.Rating = Rating;
        }
        public void Update(int Length, int Rating, string Name = "N/A", string Director = "N/A")
        {
            this.Name = Name;
            this.Length = Length;
            this.Director = Director;
            this.Rating = Rating;
        }
    }
}
