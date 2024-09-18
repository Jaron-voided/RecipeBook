namespace RecipeBook.Models.Measurement;

public class MeasurementDTO
{
    public string RecipeName { get; set; }
    public string IngredientName { get; set; }  // Instead of full IIngredient, just expose the name
    public decimal Amount { get; set; }
    public decimal Price { get; set; }  // Price based on the calculation in the Measurement interface

    // Constructor
    public MeasurementDTO(string recipeName, string ingredientName, decimal amount, decimal price)
    {
        RecipeName = recipeName;
        IngredientName = ingredientName;
        Amount = amount;
        Price = price;
    }

    // Parameterless constructor (useful for deserialization)
    public MeasurementDTO() { }
}
