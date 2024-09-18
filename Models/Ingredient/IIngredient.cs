namespace RecipeBook.Models.Ingredient;

using Extras;
public interface IIngredient
{
    // Properties
    public string Name { get; set; }
    public Categories.MeasuredIn MeasuredIn { get; set; } 
    public Categories.IngredientCategory IngredientCategory { get; set; }
    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }

    // Calculated Properties
    public decimal PricePerMeasurement { get; }

    // Methods
    decimal PricePerPortion(decimal portion);

    // Overrides
    string ToString();

    // I could instead use this interface and store less data
    // But I would lose the ability to have the extra data
    // public string? Name { get; set; }
    // public MeasuredIn MeasuredIn { get; set; } // weight, volume or "eaches"
    // public decimal PricePerMeasurement { get; set; } //This will keep from storing pricePerCase and whatnot
    
    // // Methods
    // decimal PricePerPortion(decimal portion);

    // // Overrides
    // string ToString();
}