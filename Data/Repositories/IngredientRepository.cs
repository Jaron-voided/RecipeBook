namespace RecipeBook;
using System.Data.Common;
using Models.Ingredient;

// IngredientRepository.cs
public class IngredientRepository : IIngredientRepository
{
    private readonly DbConnection _connection;

    public IngredientRepository(DbConnection connection)
    {
        _connection = connection;
    }

    public Ingredient GetIngredientById(int id)
    {
        // Implementation of SQL query to fetch ingredient by ID
    }

    public IEnumerable<Ingredient> GetAllIngredients()
    {
        // Implementation of SQL query to fetch all ingredients
    }

    public void AddIngredient(Ingredient ingredient)
    {
        // Implementation to add an ingredient to the database
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        // Implementation to update an ingredient in the database
    }

    public void DeleteIngredient(int id)
    {
        // Implementation to delete an ingredient from the database
    }
}

