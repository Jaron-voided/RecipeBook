namespace RecipeBook.Models.Ingredient;

public class IngredientDTO
{
    public string Name { get; set; }
    public string MeasuredIn { get; set; } // Can map enum to string for easy readability
    public string IngredientCategory { get; set; }
    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }
}
