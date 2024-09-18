using System.Data.Common;
using RecipeBook.Models.Recipe;
namespace RecipeBook.Data.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DbConnection _connection;

    public RecipeRepository(DbConnection connection)
    {
        _connection = connection;
    }

    public Recipe GetRecipeById(int id)
    {
        // Implementation of SQL query to fetch ingredient by ID
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        // Implementation of SQL query to fetch all ingredients
    }

    public IEnumerable<Recipe> GetRecipesByCategory(Models.Extras.Categories.RecipeCategory category)
    {
        // Implementation to add an ingredient to the database
    }
    public void AddRecipe(Recipe recipe)
    {
        // Implement to add a recipe
    }

    public void UpdateRecipe(Recipe recipe)
    {
        // Implementation to update an ingredient in the database
    }

    public void DeleteRecipe(int id)
    {
        // Implementation to delete an ingredient from the database
    }
}
