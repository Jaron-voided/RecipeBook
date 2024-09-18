using System.Data.Common;
using RecipeBook.Models.Measurement;
namespace RecipeBook;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly DbConnection _connection;

    public MeasurementRepository(DbConnection connection)
    {
        _connection = connection;
    }

    public Measurement GetMeasurementById(int id)
    {
        // Implementation of SQL query to fetch ingredient by ID
    }

    public IEnumerable<Measurement> GetMeasurementsByRecipeId()
    {
        // Implementation of SQL query to fetch all ingredients
    }

    public void AddMeasurement(Measurement measurement)
    {
        // Implementation to add an ingredient to the database
    }

    public void UpdateMeasurement(Measurement measurement)
    {
        // Implementation to update an ingredient in the database
    }

    public void DeleteMeasurement(int id)
    {
        // Implementation to delete an ingredient from the database
    }
}
