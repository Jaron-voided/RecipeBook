namespace RecipeBook;

using Models.Ingredient;
// IIngredientRepository.cs
public interface IIngredientRepository
{
    Ingredient GetIngredientById(int id);
    IEnumerable<Ingredient> GetAllIngredients();
    void AddIngredient(Ingredient ingredient);
    void UpdateIngredient(Ingredient ingredient);
    void DeleteIngredient(int id);
}

