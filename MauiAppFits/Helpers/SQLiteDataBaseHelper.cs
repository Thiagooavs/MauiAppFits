using MauiAppFits.Models;
using SQLite;
namespace MauiAppFits.Helpers

{
    public class SQLiteDataBaseHelper
    {

        readonly SQLiteAsyncConnection _db;

        public SQLiteDataBaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Atividade>().Wait();
        }
    }
}
