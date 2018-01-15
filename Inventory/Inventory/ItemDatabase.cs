using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    class ItemDatabase
    {
        private SQLiteAsyncConnection database;
        public ItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ItemData>().Wait();
        }
        public Task<List<ItemData>> GetItemsAsync()
        {
            return database.Table<ItemData>().ToListAsync();
        }
        public Task<int> SaveItemAsync(ItemData item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(ItemData item)
        {
            return database.DeleteAsync(item);
        }
    }
}
