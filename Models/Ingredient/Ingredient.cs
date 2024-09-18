namespace RecipeBook.Models.Ingredient;
using Extras;
public class Ingredient : IIngredient
{
    // Properties
    public string Name { get; set; }
    public Categories.MeasuredIn MeasuredIn { get; set; } 
    public Categories.IngredientCategory IngredientCategory { get; set; }

    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }

    // Calculated Properties
    public decimal PricePerMeasurement => MeasurementsPerPackage != 0 ? PricePerPackage / PricePerMeasurement : 0;
    // {
    //     get
    //     {
    //         // Avoid dividing by zero, if MeasurementPerPackage == 0, return 0
    //         return MeasurementsPerPackage != 0 ? PricePerPackage / PricePerMeasurement : 0;
    //     }
    // }

    // Parameterless constructor
    public Ingredient() { }

    // Constructor
    public Ingredient(string name, Categories.MeasuredIn measuredIn, Categories.IngredientCategory ingredientCategory, decimal pricePerPackage, int measurementsPerPackage)
    {
        Name = name;
        MeasuredIn = measuredIn;
        IngredientCategory = ingredientCategory;
        PricePerPackage = pricePerPackage;
        MeasurementsPerPackage = measurementsPerPackage;
    }

    // Methods
    public decimal PricePerPortion(decimal portion)
    {
        return PricePerMeasurement * portion;
    }

    // Overrides
    public override string ToString()
    {
        return $"Ingredient Name: {Name} " + $"{PricePerMeasurement} dollars per {MeasuredIn.ToString()}";
    }
}