using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_23012018
{
    public class MovieDatabase
    {
        private SQLiteAsyncConnection database;
        public MovieDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Movie>().Wait();
        }
        public Task<List<Movie>> GetAllItemsAsync()
        {
            return database.Table<Movie>().ToListAsync();
        }
        public Task<List<Movie>> GetItemsSortAsync(string sort)
        {
            return database.QueryAsync<Movie>("SELECT * FROM [Movie] ORDER BY [Rating] " + sort);
        }
        /*public Task<List<Movie>> GetItemsDscAsync()
        {
            return database.QueryAsync<Movie>("SELECT * FROM [Movie] ORDER BY [Rating] DESC");
        }*/
        public Task<int> SaveItemAsync(Movie newItem)
        {
            foreach (Movie item in GetAllItemsAsync().Result)
            {
                if (item.ID == newItem.ID)
                {
                    return database.UpdateAsync(newItem);
                }
            }
            return database.InsertAsync(newItem);
        }
        public Task<List<Movie>> DeleteItemAsync(Movie item)
        {
            return database.QueryAsync<Movie>("DELETE FROM [Movie] WHERE [ID] = '" + item.ID + "'");
        }
    }
}
