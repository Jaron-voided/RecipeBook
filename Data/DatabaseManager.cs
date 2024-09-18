using RecipeBook.Data.Sqlite;

namespace RecipeBook.Data
{
    public class DatabaseManager
    {
        public IPantryDatabase GetPantryDatabase()
        {
            return new SqlitePantryDatabase();
        }
    }
}
