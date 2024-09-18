namespace RecipeBook.Models.Measurement;
using Ingredient;


public interface IMeasurement
{
    // Properties
    public string RecipeName { get; set; }
    public IIngredient Ingredient { get; set; }
    public decimal Amount { get; set; }
    
    // Calculated Property
    public decimal Price => Ingredient.PricePerPortion(Amount);
}