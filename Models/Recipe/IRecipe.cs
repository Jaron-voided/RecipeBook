namespace RecipeBook.Models.Recipe;
using Measurement;
using Extras;

public interface IRecipe
{
    // Properties
    string Name { get; set; }
    List<IMeasurement> Measurements { get; set; }
    public Categories.RecipeCategory RecipeCategory { get; set; }

    List<string> Instructions { get; set; }
    int ServingsPerRecipe { get; set; }
    
    // Calculated Properties
    float TotalPriceForRecipe { get; }
    
    float PricePerServing { get; }
    // Additional methods
    string InstructionsToJson();
    
    // overrides
    string ToString();
}