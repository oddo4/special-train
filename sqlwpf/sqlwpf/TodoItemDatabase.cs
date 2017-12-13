using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlwpf
{
    class TodoItemDatabase
    {
        private SQLiteAsyncConnection database;
        public TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
        }
        public Task<List<TodoItem>> GetItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }
        public Task<List<TodoItem>> GetItemsManufactureAsync(string m)
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Manufacture] = '" + m + "'"); // klasické SQL
        }
        public Task<TodoItem> GetItemAsync(string m)
        {
            return database.Table<TodoItem>().Where(i => i.Manufacture == m).FirstOrDefaultAsync(); // LINQ syntaxe
        }
        public Task<int> SaveItemAsync(TodoItem item)
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
        public Task<List<TodoItem>> DeleteItemAsync(TodoItem item)
        {
            return database.QueryAsync<TodoItem>("DELETE FROM [TodoItem] WHERE [ID] = " + item.ID);
        }
    }
}
