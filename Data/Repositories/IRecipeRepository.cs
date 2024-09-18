using RecipeBook.Models.Recipe;

namespace RecipeBook;

public interface IRecipeRepository
{
    Recipe GetRecipeById(int id);
    IEnumerable<Recipe> GetAllRecipes();
    IEnumerable<Recipe> GetRecipesByCategory(Models.Extras.Categories.RecipeCategory category);
    void AddRecipe(Recipe recipe);
    void UpdateRecipe(Recipe recipe);
    void DeleteRecipe(int id);
}

