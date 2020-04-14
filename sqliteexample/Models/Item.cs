using SQLite;

namespace sqliteexample.Models
{
    // Tell SQLite that this class is to be stored in a table called Items.
    [Table("Items")]
    public class Item
    {
        // Tell SQLite that Id is the Primary Key for this table, and to autoincrement it.
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}