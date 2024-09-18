namespace RecipeBook.Services;
using RecipeBook.Models.Recipe;
using RecipeBook.Models.Ingredient;

public static class RecipeMapper
{
    public static RecipeDTO MapToDTO(Recipe recipe)
    {
        return new RecipeDTO
        {
            Name = recipe.Name,
            Measurements = recipe.Measurements,
            RecipeCategory = recipe.RecipeCategory,
            Instructions = recipe.Instructions,
            ServingsPerRecipe = recipe.ServingsPerRecipe
        };
    }

    public static Recipe MapToModel(RecipeDTO dto)
    {
        return new Recipe
        {
            Name = dto.Name,
            Measurements = dto.Measurements,
            RecipeCategory = dto.RecipeCategory,
            Instructions = dto.Instructions,
            ServingsPerRecipe = dto.ServingsPerRecipe
        };
    }
}
