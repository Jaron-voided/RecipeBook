using RecipeBook.Models.Measurement;

namespace RecipeBook;

public interface IMeasurementRepository
{
    Measurement GetMeasurementById(int id);
    IEnumerable<Measurement> GetMeasurementsByRecipeId(int recipeId);
    void AddMeasurement(Measurement measurement);
    void UpdateMeasurement(Measurement measurement);
    void DeleteMeasurement(int id);
}

