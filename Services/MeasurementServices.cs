namespace RecipeBook.Services;
using Models.Measurement;
using Models.Ingredient;

public static class MeasurementMapper
{
    public static MeasurementDTO MapToDTO(IMeasurement measurement)
    {
        return new MeasurementDTO
        {
            RecipeName = measurement.RecipeName,
            IngredientName = measurement.Ingredient.Name,
            Amount = measurement.Amount,
            Price = measurement.Price // Already derived
        };
    }

    public static IMeasurement MapToModel(MeasurementDTO dto, IIngredient ingredient)
    {
        return new Measurement
        {
            RecipeName = dto.RecipeName,
            Ingredient = ingredient,
            Amount = dto.Amount
        };
    }
}
