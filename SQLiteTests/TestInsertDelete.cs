using NUnit.Framework;
using sqliteexample.Models;
using sqliteexample.Services;
using System.Linq;
namespace SQLiteTests
{
    [TestFixture()]
    public class TestInsertDelete
    {
        SQLiteDataStore dataStore;

        [SetUp()]
        public void SetUpDatabase()
        {
            dataStore = new SQLiteDataStore();
        }

        [Test()]
        public void TestInsert()
        {
            var item = new Item { Text = "Some Text", Description = "Some description" };
            var number_inserted = dataStore.AddItemAsync(item).Result;

            var all_items = dataStore.GetItemsAsync().Result.ToList<Item>() ;
            Assert.AreEqual(item.Id, all_items[all_items.Count-1].Id);
        }
        [Test()]
        public void TestDelete()
        {
            var all_items_before = dataStore.GetItemsAsync().Result.ToList<Item>();

            var item = new Item { Text = "Some Text", Description = "Some description" };
            var number_inserted = dataStore.AddItemAsync(item).Result;
            var deleted = dataStore.DeleteItemAsync(item.Id);
            Assert.AreEqual(true, deleted);

            var all_items_after = dataStore.GetItemsAsync().Result.ToList<Item>();
            Assert.AreEqual(all_items_before.Count, all_items_after.Count);
        }

    }
}
