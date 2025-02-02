﻿using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.IO;
using System.Reflection;
using System.Text.Json;


namespace RecipeApp_labb3Db.Presentation.Services
{
    public class JsonService
    {
        

        public List<Unit> LoadUnits()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "units.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                List<string> unitNames = JsonSerializer.Deserialize<List<string>>(json);

                List<Unit> units = unitNames.Select(unitName => new Unit { UnitName = unitName, Amount = 0 }).ToList();
                return units;
                
            }
            else
            {
                throw new FileNotFoundException("Filen hittades inte (enheter)", filePath);
            }
        }

        public List<Ingredient> LoadIngredient()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "Ingredients.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json);
                ingredients.ForEach(i => i.Name.ToLower());
                

                return new List<Ingredient>(ingredients);
            }
            else
            {
                throw new FileNotFoundException("Filen hittades inte ", filePath);
            }
        }

        public async Task LoadDemoRecipe()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "Recipe.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var recipe = JsonSerializer.Deserialize<Recipe>(json);
                var recipeDataAccess = new RecipeDataAccess();
                await recipeDataAccess.CreateRecipe(recipe);
                
            }
            else
            {
                throw new FileNotFoundException("Filen hittades inte", filePath);
            }
        }

    }
}
