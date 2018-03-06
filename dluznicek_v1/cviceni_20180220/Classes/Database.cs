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
        private SQLiteConnection database;
        public Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Item>();
            database.CreateTable<ItemsList>();
            database.CreateTable<ItemTies>();
            database.CreateTable<Transaction>();
        }
        public List<Item> GetItemAsync()
        {
            return database.Table<Item>().ToList();
        }
        public List<Item> GetItemsAsync(int idList)
        {
            List<Item> list = new List<Item>();
            var result = database.Query<ItemTies>("SELECT * FROM [ItemTies] WHERE [IDItemsList] = '" + idList + "'");
            foreach (ItemTies tie in result)
            {
                var item = database.Query<Item>("SELECT * FROM [Item] WHERE [ID] = '" + tie.IDItem + "'").FirstOrDefault();
                list.Add(item);
            }
            return list;
        }
        public List<Transaction> GetTransactionAsync()
        {
            return database.Table<Transaction>().ToList();
        }
        public List<Item> GetItemTotalYear()
        {
            List<Item> items = new List<Item>();
            var transaction = GetTransactionAsync();
            var today = DateTime.Today;
            foreach (Transaction trans in transaction)
            {
                if (trans.DateTransaction.Year == today.Year /*&& trans.DateTransaction.Month <= today.Month*/)
                {
                    items.Add(database.Query<Item>("SELECT * FROM [Item] WHERE [ID] = '" + trans.IDItem + "'").FirstOrDefault());
                }
            }
            return items;
        }
        public List<Item> GetItemTotalMonth()
        {
            List<Item> items = new List<Item>();
            var transaction = GetTransactionAsync();
            var today = DateTime.Today;
            foreach (Transaction trans in transaction)
            {
                if (trans.DateTransaction.Year == today.Year && trans.DateTransaction.Month == today.Month /*&& trans.DateTransaction <= today*/)
                {
                    items.Add(database.Query<Item>("SELECT * FROM [Item] WHERE [ID] = '" + trans.IDItem + "'").FirstOrDefault());
                }
            }
            return items;
        }
        public List<ItemsList> GetItemsListsAsync()
        {
            return database.Table<ItemsList>().ToList();
        }
        public ItemsList GetItemsList(int idItemsList)
        {
            return database.Query<ItemsList>("SELECT * FROM [ItemsList] WHERE [ID] = '" + idItemsList + "'").FirstOrDefault();
        }
        public List<ItemTies> GetItemTiesAsync()
        {
            return database.Table<ItemTies>().ToList();
        }
        public Transaction GetTransaction(int idItem)
        {
            return database.Query<Transaction>("SELECT * FROM [Transaction] WHERE [IDItem] = '" + idItem + "'").FirstOrDefault();
        }
        public int SaveItemAsync(Item item)
        {
            var result = GetItemAsync();
            foreach (Item i in result)
            {
                if (item.ID == i.ID)
                {
                    return database.Update(item);
                }
            }

            return database.Insert(item);
        }
        public int SaveTransactionAsync(Transaction transaction)
        {
            var result = database.Table<Transaction>().ToList();
            foreach (Transaction i in result)
            {
                if (transaction.ID == i.ID)
                {
                    return database.Update(transaction);
                }
            }

            return database.Insert(transaction);
        }
        public int SaveItemTiesAsync(ItemTies itemTies)
        {
            var result = GetItemTiesAsync();
            foreach (ItemTies i in result)
            {
                if (itemTies.ID == i.ID)
                {
                    return database.Update(itemTies);
                }
            }

            return database.Insert(itemTies);
        }
        public int SaveItemsListAsync(ItemsList itemsList)
        {
            var result = GetItemsListsAsync();
            foreach (ItemsList i in result)
            {
                if (itemsList.ID == i.ID)
                {
                    return database.Update(itemsList);
                }
            }

            return database.Insert(itemsList);
        }
        public void DeleteItem(Item item)
        {
            database.Delete(item);
            database.Query<ItemTies>("DELETE FROM [ItemTies] WHERE [IDItem] = " + item.ID);
            database.Query<Transaction>("DELETE FROM [Transaction] WHERE [IDItem] = " + item.ID);
        }
        public void DeleteItemsList(ItemsList itemsList)
        {
            database.Delete(itemsList);
            database.Query<ItemTies>("DELETE FROM [ItemTies] WHERE [IDItemsList] = " + itemsList.ID);
        }
        public void DeleteTables()
        {
            database.Query<Item>("DELETE FROM [Item]");
            database.Query<ItemsList>("DELETE FROM [ItemsList]");
            database.Query<ItemTies>("DELETE FROM [ItemTies]");
        }
    }
}
