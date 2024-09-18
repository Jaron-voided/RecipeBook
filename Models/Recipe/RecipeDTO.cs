namespace RecipeBook.Models.Recipe;

using Measurement;
using Extras;
public class RecipeDTO
{
        // Properties
    public string Name { get; set; }
    public List<IMeasurement> Measurements { get; set; }
    public Categories.RecipeCategory RecipeCategory { get; set; }

    public List<string> Instructions { get; set; }
    public int ServingsPerRecipe { get; set; }

}
