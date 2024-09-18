namespace RecipeBook.Services;
using RecipeBook.Models.Extras;
using RecipeBook.Models.Ingredient;

public static class IngredientMapper
{
    public static IngredientDTO MapToDTO(IIngredient ingredient)
    {
        return new IngredientDTO
        {
            Name = ingredient.Name,
            MeasuredIn = ingredient.MeasuredIn.ToString(),  // Converts enum to string
            IngredientCategory = ingredient.IngredientCategory.ToString(),  // Converts enum to string
            PricePerPackage = ingredient.PricePerPackage,
            MeasurementsPerPackage = ingredient.MeasurementsPerPackage,
        };
    }

    public static IIngredient MapToModel(IngredientDTO dto)
    {
        return new Ingredient
        {
            Name = dto.Name,
            MeasuredIn = Enum.Parse<Models.Extras.Categories.MeasuredIn>(dto.MeasuredIn),  // Converts string back to enum
            IngredientCategory = Enum.Parse<Models.Extras.Categories.IngredientCategory>(dto.IngredientCategory),
            PricePerPackage = dto.PricePerPackage,
            MeasurementsPerPackage = dto.MeasurementsPerPackage
        };
    }
}
