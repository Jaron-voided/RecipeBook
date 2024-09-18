namespace RecipeBook;

// IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IIngredientRepository Ingredients { get; }
    IRecipeRepository Recipes { get; }
    IMeasurementRepository Measurements { get; }

    void Commit();  // Save changes to the database
}
