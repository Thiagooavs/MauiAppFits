using MauiAppFits.Helpers;
using MauiAppFits.Models;
namespace MauiAppFits
{
    public partial class App : Application
    {
        
         static SQLiteDataBaseHelper database;

        public static SQLiteDataBaseHelper DataBase
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteDataBaseHelper(
                        Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                            "XamAppFit.db3"));

                    
                }
                return database;
            }
            
                               
        }


        
    }
}