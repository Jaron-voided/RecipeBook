namespace RecipeBook.Models.Measurement;
using Ingredient;


public class Measurement : IMeasurement
{
    // Properties
    public string RecipeName { get; set; }
    public IIngredient Ingredient { get; set; }
    public decimal Amount { get; set; }
    
    // Calculated Property
    public decimal Price => Ingredient.PricePerPortion(Amount);

    // Constructor
    public Measurement(string recipeName, IIngredient ingredient, decimal amount)
    {
        RecipeName = recipeName;
        Ingredient = ingredient;
        Amount = amount;
    }
}