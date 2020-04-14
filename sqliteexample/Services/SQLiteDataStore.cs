using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using sqliteexample.Models;

namespace sqliteexample.Services
{
    // Implement the same interface as MockDataStore, but actually store
    // things in a SQLite database.
    public class SQLiteDataStore : IDataStore<Item>
    {
        const string DBName = "ExampleDatabase.db";
        readonly SQLiteAsyncConnection connection;
        public SQLiteDataStore()
        {
            connection = new SQLiteAsyncConnection(DBName);
            // Make a table if it doesn't already exist.  Do it synchronously.
            connection.CreateTableAsync<Item>().Wait();
        }

        public async Task<int> AddItemAsync(Item item)
        {
            return await connection.InsertAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item_to_delete = connection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
            var number_deleted = await connection.DeleteAsync(item_to_delete.Result);

            return number_deleted == 1;
        }

        public Task<Item> GetItemAsync(int id)
        {
            return connection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await connection.Table<Item>().OrderBy(xx => xx.Id).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var itemsUpdated = await connection.UpdateAsync(item);

            return itemsUpdated == 1;
        }
    }
}
