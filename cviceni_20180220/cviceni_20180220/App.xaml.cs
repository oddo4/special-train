using cviceni_20180220.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace cviceni_20180220
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database("FileData.db3");
                }
                return database;
            }
        }
        public static LoadDatasInListView LoadDatasInListView = new LoadDatasInListView();
    }
}
