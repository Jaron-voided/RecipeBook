using System.Data.Common;
using RecipeBook.Data.Repositories;

namespace RecipeBook;

// UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection _connection;
    private DbTransaction _transaction;

    public IIngredientRepository Ingredients { get; private set; }
    public IRecipeRepository Recipes { get; private set; }
    public IMeasurementRepository Measurements { get; private set; }

    public UnitOfWork(DbConnection connection)
    {
        _connection = connection;
        _transaction = _connection.BeginTransaction();
        Ingredients = new IngredientRepository(_connection);
        Recipes = new RecipeRepository(_connection);
        Measurements = new MeasurementRepository(_connection);
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _connection.Dispose();
    }
}
