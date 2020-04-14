# CS0305 SQLite Example

This repository has a simple example of a SQLite implementation for one of the Xamarin stub projects.

The code uses the `sqlite-net-pcl package`.

## Model Annotations

The model needs to have an integer public property for this example.  The `Table` annotation tells SQLite 
what table the model class belongs to.  The `PrimaryKey` annotation on the integer property tells SQLite which 
property is the primary key (unique for each entry in the database).  The `AutoIncrement` annotation tells SQLite that
this field in the database will be incremented automatically by the database on inserts where it is not set.

```C#
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
````

## Getting A Database Connection

The database connection is a `SQLiteAsyncConnection` and is had simply by:

```C#
connection = new SQLiteAsyncConnection(DBName);
```

## Creating `Items` Table

The table is created by:

```C#
connection.CreateTableAsync<Item>().Wait();
```

I've chosen to make this asynchronous call synchronous to ensure the tabe exists before any
other calls are made.

## Inserting An `Item`

Items can then just be inserted using:

```C#
connection.InsertAsync(item);
```

## Getting All Items

All `Item`s in the database can be retrieved using:

```C#
 connection.Table<Item>().OrderBy(xx => xx.Id).ToListAsync();
 ```
