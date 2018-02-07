using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180207
{
    public class Databaze
    {
        private SQLiteAsyncConnection database;
        public Databaze(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Predmet>().Wait();
            database.CreateTableAsync<Student>().Wait();
            database.CreateTableAsync<Znamka>().Wait();
            database.CreateTableAsync<Trida>().Wait();
        }
        public Task<List<Predmet>> GetPredmetAsync()
        {
            return database.Table<Predmet>().ToListAsync();
        }
        public Task<List<Student>> GetStudentAsync()
        {
            return database.Table<Student>().ToListAsync();
        }
        public Task<List<Trida>> GetTridaAsync()
        {
            return database.Table<Trida>().ToListAsync();
        }
        public Task<List<Znamka>> GetZnamkyAsync(int id)
        {
            return database.QueryAsync<Znamka>("SELECT * FROM [Znamka] WHERE [IDStudent] = '" + id + "'");
        }
        public Task<List<Student>> FindStudentAsync(int id)
        {
            return database.QueryAsync<Student>("SELECT * FROM [Student] WHERE [ID] = '" + id + "'");
        }
        public Task<List<Trida>> FindTridaAsync(string t)
        {
            return database.QueryAsync<Trida>("SELECT * FROM [Trida] WHERE [Nazev] = '" + t + "'");
        }
        public Task<Trida> GetTridaName(int id)
        {
            return database.Table<Trida>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Predmet> GetPredmetName(int id)
        {
            return database.Table<Predmet>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Trida> GetTridaId(string t)
        {
            return database.Table<Trida>().Where(i => i.Nazev == t).FirstOrDefaultAsync();
        }
        public Task<int> SavePredmetAsync(Predmet item)
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
        public Task<int> SaveTridaAsync(Trida item)
        {
            var result = GetTridaAsync().Result;
            foreach (Trida t in result)
            {
                if(item.ID == t.ID)
                {
                    return database.UpdateAsync(item);
                    
                }
            }

            return database.InsertAsync(item);

        }
        public Task<int> SaveStudentAsync(Student item)
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
        public Task<int> SaveZnamkaAsync(Znamka item)
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
        public void DeleteStudent(int id)
        {
            database.QueryAsync<Student>("DELETE FROM [Student] WHERE [ID] = " + id);
        }
        public void DeleteTables()
        {
            database.QueryAsync<Trida>("DELETE FROM [Trida]");
            database.QueryAsync<Student>("DELETE FROM [Student]");
            database.QueryAsync<Znamka>("DELETE FROM [Znamka]");
            database.QueryAsync<Predmet>("DELETE FROM [Predmet]");
        }
    }
}
