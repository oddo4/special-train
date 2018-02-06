using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180206
{
    class Database
    {
        private SQLiteAsyncConnection database;
        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
            database.CreateTableAsync<Category>().Wait();
            database.CreateTableAsync<ItemTies>().Wait();
        }
        public Task<List<Item>> GetDefinedItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }
        public Task<List<Category>> GetCategoriesAsync()
        {
            return database.Table<Category>().ToListAsync();
        }
        public Task<List<ItemTies>> GetItemsAsync(int c)
        {
            return database.QueryAsync<ItemTies>("SELECT * FROM [ItemTies] WHERE [IDCategory] = '" + c + "'");
        }
        public Task<List<Item>> GetDefinedItemAsync(int id)
        {
            return database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [ID] = '" + id + "'");
        }
        public Task<int> SaveItemAsync(Item item)
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
        public Task<int> SaveCategoryAsync(Category category)
        {
            if (category.ID != 0)
            {
                return database.UpdateAsync(category);
            }
            else
            {
                return database.InsertAsync(category);
            }
        }
        public Task<int> SaveItemTiesAsync(ItemTies category)
        {
            if (category.ID != 0)
            {
                return database.UpdateAsync(category);
            }
            else
            {
                return database.InsertAsync(category);
            }
        }
        public Task<List<Category>> DeleteCategoryAsync(Category cat)
        {
            database.QueryAsync<ItemTies>("DELETE FROM [ItemTies] WHERE [IDCategory] = " + cat.ID);
            return database.QueryAsync<Category>("DELETE FROM [Category] WHERE ID = " + cat.ID);
        }
        public Task<List<ItemTies>> DeleteItemAsync(Category cat, Item item)
        {
            var tie = database.Table<ItemTies>().Where(i => i.IDCategory == cat.ID && i.IDItem == item.ID).FirstOrDefaultAsync().Result;

            return database.QueryAsync<ItemTies>("DELETE FROM [ItemTies] WHERE ID = " + tie.ID);
        }
    }
}
