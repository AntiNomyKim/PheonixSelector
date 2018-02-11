using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region [Category]

        public Task<List<Category>> GetCategoryListAsync()
        {
            return db.Table<Category>().ToListAsync();
        }

        public Task<List<Category>> GetCategoryListNotDoneAsync(string code)
        {
            return db.QueryAsync<Category>("SELECT * FROM [Category] WHERE [CategoryCode] = " + code);
        }

        public Task<Category> GetCategoryAsync(string code)
        {
            return db.Table<Category>().Where(x => x.CategoryCode == code).FirstOrDefaultAsync();
        }

        public Task<int> SaveCategoryAsync(Category category)
        {
            var dataDup = false;
            
            //중복확인
            List<Category> categoryList = GetCategoryListAsync().Result;

            var cnt = categoryList.Where((x) => x.CategoryCode == category.CategoryCode).Count();
            dataDup = cnt == 0 ? false : true;

            if (dataDup)
            {
                return db.UpdateAsync(category);
            }
            else
            {
                return db.InsertAsync(category);
            }
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return db.DeleteAsync(category);
        }

        public string GetMaximumCategoryCode()
        {
            return (GetCategoryListAsync().Result as List<Category>).Count.ToString();
        }

        #endregion [Category]

        #region [Item]

        public Task<List<Item>> GetItemListAsync()
        {
            return db.Table<Item>().ToListAsync();
        }

        public Task<List<Item>> GetItemListNotDoneAsync(string categoryCode)
        {
            return db.QueryAsync<Item>("SELECT * FROM [Item] WHERE [CategoryCode] = " + categoryCode);
        }

        public Task<Item> GetItemAsync(string itemCode)
        {
            return db.Table<Item>().Where(x => x.ItemCode == itemCode).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            var dataDup = false;
            
            //중복확인
            List<Item> itemList = GetItemListAsync().Result;

            var cnt = itemList.Where((x) => x.ItemCode == item.ItemCode).Count();
            dataDup = cnt == 0 ? false : true;

            if (dataDup)
            {
                return db.UpdateAsync(item);
            }
            else
            {
                return db.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Item Item)
        {
            return db.DeleteAsync(Item);
        }

        public string GetMaximumItemCode()
        {
            return (GetItemListAsync().Result as List<Item>).Count.ToString();
        }

        #endregion [Item]
    }
}
