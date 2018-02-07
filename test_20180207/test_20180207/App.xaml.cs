using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace test_20180207
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Databaze databaze;
        public static Databaze Databaze
        {
            get
            {
                if (databaze == null)
                {
                    databaze = new Databaze("FileData.db3");
                }
                return databaze;
            }
        }
    }
}
