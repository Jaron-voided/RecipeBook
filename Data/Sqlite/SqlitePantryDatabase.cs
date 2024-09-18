using System.Data.SQLite;
using System.Text.Json;
using OfficeOpenXml;
using RecipeBook.Data;
using RecipeBook.Models.Recipe;
using RecipeBook.Models.Ingredient;
using RecipeBook.Models.Measurement;
using RecipeBook.Models.Extras;

namespace RecipeBook.Data.Sqlite
{
    public class SqlitePantryDatabase : IPantryDatabase
    {
        // Ingredients
        public void CreateIngredientTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Ingredient (
                IngredientName TEXT PRIMARY KEY,
                MeasuredIn INTEGER,
                PricePerPackage DECIMAL,
                MeasurementsPerPackage INTEGER
            );";

            DB.CreateTable(createTableQuery);
        }

        public void InsertIngredient(IIngredient ingredient)
        {
            const string insertCommandText = "INSERT INTO Ingredient (IngredientName, MeasuredIn, PricePerPackage, MeasurementsPerPackage) " +
                                             "VALUES (@name, @measuredIn, @pricePerPackage, @measurementsPerPackage);";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@name", ingredient.Name),
                new SQLiteParameter("@measuredIn", (int)ingredient.MeasuredIn),
                new SQLiteParameter("@pricePerPackage", ingredient.PricePerPackage),
                new SQLiteParameter("@measurementsPerPackage", ingredient.MeasurementsPerPackage)
            };

            DB.ExecuteNonQuery(insertCommandText, parameters);
        }

        public void UpdateIngredient(IIngredient ingredient)
        {
            const string updateCommandText = "UPDATE Ingredient SET MeasuredIn = @measuredIn, PricePerPackage = @pricePerPackage, MeasurementsPerPackage = @measurementsPerPackage " +
                                             "WHERE IngredientName = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@measuredIn", (int)ingredient.MeasuredIn),
                new SQLiteParameter("@pricePerPackage", ingredient.PricePerPackage),
                new SQLiteParameter("@measurementsPerPackage", ingredient.MeasurementsPerPackage),
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(updateCommandText, parameters);
        }

        public void DeleteIngredient(IIngredient ingredient)
        {
            const string deleteCommandText = "DELETE FROM Ingredient WHERE IngredientName = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(deleteCommandText, parameters);
        }

        public IEnumerable<IIngredient> GetAllIngredients()
        {
            const string selectCommandText = "SELECT * FROM Ingredient;";

            var ingredients = new List<IIngredient>();

            using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SQLiteParameter>());
            while (reader.Read())
            {
                var name = reader.GetString(reader.GetOrdinal("IngredientName"));
                var measuredIn = (Categories.MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
                var pricePerPackage = reader.GetDecimal(reader.GetOrdinal("PricePerPackage"));
                var measurementsPerPackage = reader.GetInt32(reader.GetOrdinal("MeasurementsPerPackage"));

                var ingredient = new Ingredient(name, measuredIn, Categories.IngredientCategory.Spice, pricePerPackage, measurementsPerPackage);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public void ExportIngredientsToExcel(string excelFilePath)
        {
            using var package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Ingredients");

            // Set column headers
            worksheet.Cells["A1"].Value = "Name";
            worksheet.Cells["B1"].Value = "Measured In";
            worksheet.Cells["C1"].Value = "Price Per Package";
            worksheet.Cells["D1"].Value = "Measurements Per Package";

            var row = 2;

            using (var reader = DB.ExecuteReader("SELECT * FROM Ingredient;", Array.Empty<SQLiteParameter>()))
            {
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var measuredInValue = reader.GetInt32(1); // Store the integer value for now
                    var pricePerPackage = reader.GetDecimal(2);
                    var measurementsPerPackage = reader.GetInt32(3);

                    var measuredIn = Enum.GetName(typeof(Categories.MeasuredIn), measuredInValue); // Get the enum name

                    // Populate the Excel rows with ingredient data
                    worksheet.Cells[row, 1].Value = name;
                    worksheet.Cells[row, 2].Value = measuredIn;
                    worksheet.Cells[row, 3].Value = pricePerPackage;
                    worksheet.Cells[row, 4].Value = measurementsPerPackage;

                    row++;
                }
            }

            package.SaveAs(new FileInfo(excelFilePath));
        }

        // Measurements
        public void CreateMeasurementTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Measurement (
                RecipeName TEXT,
                IngredientName TEXT,
                Amount DECIMAL,
                FOREIGN KEY (RecipeName) REFERENCES Recipe(RecipeName) ON DELETE CASCADE ON UPDATE CASCADE,
                FOREIGN KEY (IngredientName) REFERENCES Ingredient(IngredientName) ON DELETE CASCADE ON UPDATE CASCADE
            );";

            DB.CreateTable(createTableQuery);
        }

        public void InsertMeasurement(IMeasurement measurement)
        {
            const string insertCommandText = "INSERT INTO Measurement (RecipeName, IngredientName, Amount) " +
                                             "VALUES (@recipeName, @ingredientName, @amount);";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@recipeName", measurement.RecipeName),
                new SQLiteParameter("@ingredientName", measurement.Ingredient?.Name),
                new SQLiteParameter("@amount", measurement.Amount)
            };

            DB.ExecuteNonQuery(insertCommandText, parameters);
        }

        // Recipes
        public void CreateRecipeTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Recipe (
                RecipeName TEXT PRIMARY KEY,
                ServingsPerRecipe INTEGER,
                Instructions TEXT,
                TotalPriceForRecipe DECIMAL,
                PricePerServing DECIMAL
            );";

            DB.CreateTable(createTableQuery);
        }

        public void InsertRecipe(IRecipe recipe)
        {
            const string insertRecipeCommandText = "INSERT INTO Recipe (RecipeName, ServingsPerRecipe, Instructions, TotalPriceForRecipe, PricePerServing) " +
                                                   "VALUES (@name, @servingsPerRecipe, @instructions, @totalPriceForRecipe, @pricePerServing);";

            var instructions = recipe.InstructionsToJson();

            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@name", recipe.Name),
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe),
                new SQLiteParameter("@instructions", instructions),
                new SQLiteParameter("@totalPriceForRecipe", recipe.TotalPriceForRecipe),
                new SQLiteParameter("@pricePerServing", recipe.PricePerServing)
            };

            DB.ExecuteNonQuery(insertRecipeCommandText, recipeParameters);

            foreach (var measurement in recipe.Measurements)
            {
                InsertMeasurement(measurement);
            }
        }

        public void UpdateRecipe(IRecipe recipe)
        {
            const string updateRecipeCommandText = "UPDATE Recipe SET ServingsPerRecipe = @servingsPerRecipe, " +
                                                   "Instructions = @instructions, TotalPriceForRecipe = @totalPriceForRecipe, " +
                                                   "PricePerServing = @pricePerServing " +
                                                   "WHERE RecipeName = @name";

            var instructions = recipe.InstructionsToJson();

            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe),
                new SQLiteParameter("@instructions", instructions),
                new SQLiteParameter("@totalPriceForRecipe", recipe.TotalPriceForRecipe),
                new SQLiteParameter("@pricePerServing", recipe.PricePerServing),
                new SQLiteParameter("@name", recipe.Name)
            };

            DB.ExecuteNonQuery(updateRecipeCommandText, recipeParameters);
        }

        public void DeleteRecipe(IRecipe recipe)
        {
            const string deleteRecipeCommandText = "DELETE FROM Recipe WHERE RecipeName = @name;";
            SQLiteParameter[] recipeParameters = { new SQLiteParameter("@name", recipe.Name) };

            DB.ExecuteNonQuery(deleteRecipeCommandText, recipeParameters);
        }

        public IRecipe GetRecipe(string name)
        {
            const string selectCommandText = "SELECT * FROM Recipe WHERE RecipeName = @name;";

            SQLiteParameter[] recipeParameters = { new SQLiteParameter("@name", name) };

            using var reader = DB.ExecuteReader(selectCommandText, recipeParameters);

            if (!reader.Read()) return null; // Recipe not found
            var recipeName = reader.GetString(reader.GetOrdinal("RecipeName"));
            var servingsPerRecipe = reader.GetInt32(reader.GetOrdinal("ServingsPerRecipe"));
            var instructionsJson = reader.GetString(reader.GetOrdinal("Instructions"));
            var instructions = JsonSerializer.Deserialize<List<string>>(instructionsJson);
            var measurements = GetMeasurementsForRecipe(name).ToList();

            var recipe = new Recipe(recipeName, measurements, instructions, servingsPerRecipe);

            return recipe;
        }

        public IEnumerable<IRecipe> GetAllRecipes()
        {
            const string selectCommandText = "SELECT RecipeName FROM Recipe;";

            using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SQLiteParameter>());
            while (reader.Read())
            {
                var recipeName = reader.GetString(reader.GetOrdinal("RecipeName"));
                var recipe = GetRecipe(recipeName);

                if (recipe != null)
                {
                    yield return recipe;
                }
            }
        }

        public IEnumerable<IRecipe> GetRecipesContainingIngredient(string ingredientName)
        {
            const string selectCommandText = "SELECT DISTINCT RecipeName FROM Measurement WHERE IngredientName = @ingredientName;";

            SQLiteParameter[] ingredientParameters = { new SQLiteParameter("@ingredientName", ingredientName) };

            var recipeNames = new List<string>();

            using (var reader = DB.ExecuteReader(selectCommandText, ingredientParameters))
            {
                while (reader.Read())
                {
                    recipeNames.Add(reader.GetString(reader.GetOrdinal("RecipeName")));
                }
            }

            foreach (var recipe in recipeNames.Select(GetRecipe).Where(recipe => recipe != null))
            {
                yield return recipe;
            }
        }

        public IEnumerable<IRecipe> GetRecipesOrderedByPricePerServing()
        {
            var allRecipes = GetAllRecipes();
            IEnumerable<IRecipe> sortedRecipes = allRecipes.OrderBy(recipe => recipe.PricePerServing);
            return sortedRecipes;
        }

        // Private method for fetching measurements associated with a recipe
        protected IEnumerable<IMeasurement> GetMeasurementsForRecipe(string recipeName)
        {
            const string selectCommandText = "SELECT * FROM Measurement WHERE RecipeName = @recipeName;";

            SQLiteParameter[] measurementParameters = { new SQLiteParameter("@recipeName", recipeName) };

            using var reader = DB.ExecuteReader(selectCommandText, measurementParameters);

            while (reader.Read())
            {
                var ingredientName = reader.GetString(reader.GetOrdinal("IngredientName"));
                var amount = reader.GetDecimal(reader.GetOrdinal("Amount"));

                var ingredient = GetIngredientByName(ingredientName);

                if (ingredient != null)
                {
                    var measurement = new Measurement(recipeName, ingredient, amount);
                    yield return measurement;
                }
            }
        }

        // Method to create all tables
        public void CreateTable()
        {
            CreateIngredientTable();
            CreateMeasurementTable();
            CreateRecipeTable();
        }
    }
}
