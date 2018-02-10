using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PheonixSelector.Models
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Category>().Wait();
            db.CreateTableAsync<Item>().Wait();
        }
    }
}
