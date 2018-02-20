using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_20180220
{
    public class Database
    {
        private SQLiteAsyncConnection database;
        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
            database.CreateTableAsync<ItemsList>().Wait();
            database.CreateTableAsync<ItemTies>().Wait();
        }
        public Task<List<Item>> GetItemAsync()
        {
            return database.Table<Item>().ToListAsync();
        }
        public List<Item> GetItemsAsync(int idList)
        {
            List<Item> list = new List<Item>();
            var result = database.QueryAsync<ItemTies>("SELECT * FROM [ItemTies] WHERE [IDItemsList] = '" + idList + "'").Result;
            foreach (ItemTies tie in result)
            {
                var item = database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [ID] = '" + tie.IDItem + "'").Result.FirstOrDefault();
                list.Add(item);
            }
            return list;
        }
        public Task<List<ItemsList>> GetItemsListsAsync()
        {
            return database.Table<ItemsList>().ToListAsync();
        }
        public ItemsList GetItemsList(int idItemsList)
        {
            return database.QueryAsync<ItemsList>("SELECT * FROM [ItemsList] WHERE [ID] = '" + idItemsList + "'").Result.FirstOrDefault();
        }
        public Task<List<ItemTies>> GetItemTiesAsync()
        {
            return database.Table<ItemTies>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Item item)
        {
            var result = GetItemAsync().Result;
            foreach (Item i in result)
            {
                if (item.ID == i.ID)
                {
                    return database.UpdateAsync(item);
                }
            }

            return database.InsertAsync(item);
        }
        public Task<int> SaveItemTiesAsync(ItemTies itemTies)
        {
            var result = GetItemTiesAsync().Result;
            foreach (ItemTies i in result)
            {
                if (itemTies.ID == i.ID)
                {
                    return database.UpdateAsync(itemTies);
                }
            }

            return database.InsertAsync(itemTies);
        }
        public Task<int> SaveItemsListAsync(ItemsList itemsList)
        {
            var result = GetItemsListsAsync().Result;
            foreach (ItemsList i in result)
            {
                if (5 == i.ID)
                {
                    return database.UpdateAsync(itemsList);
                }
            }

            return database.InsertAsync(itemsList);
        }
        public void DeleteTables()
        {
            database.QueryAsync<Item>("DELETE FROM [Item]");
            database.QueryAsync<ItemsList>("DELETE FROM [ItemsList]");
            database.QueryAsync<ItemTies>("DELETE FROM [ItemTies]");
        }
    }
}
