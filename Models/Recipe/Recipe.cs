namespace RecipeBook.Models.Recipe;
using System.Text.Json;
using Measurement;
using Extras;
public class Recipe : IRecipe
{
    // Properties
    public string Name { get; set; }
    public List<IMeasurement> Measurements { get; set; }
    public Categories.RecipeCategory RecipeCategory { get; set; }

    public List<string> Instructions { get; set; }
    public int ServingsPerRecipe { get; set; }
    
    // Derived Properties
    public float TotalPriceForRecipe => Measurements.Sum(measurement => (float)measurement.Price);    
    public float PricePerServing => TotalPriceForRecipe / ServingsPerRecipe;

    //Constructor
    public Recipe(string name, List<IMeasurement> measurements, Categories.RecipeCategory recipeCategory, List<string> instructions, int servingsPerRecipe)
    {
        Name = name;
        Measurements = measurements ?? new List<IMeasurement>();  // Initialize if null
        RecipeCategory = recipeCategory;
        Instructions = instructions ?? new List<string>();  // Initialize if null
        ServingsPerRecipe = servingsPerRecipe;
    }


    // Additional methods
    // Method to convert Instructions to JSON
    public string InstructionsToJson()
    {
        // Serialize Instructions list to JSON
        return JsonSerializer.Serialize(Instructions);
    }
    
    // overrides
    public override string ToString()
    {
        return $"Recipe Name: {Name}. Costs : {TotalPriceForRecipe}" + 
            $"It makes {ServingsPerRecipe} servings costing  {PricePerServing} per serving";
    }
}