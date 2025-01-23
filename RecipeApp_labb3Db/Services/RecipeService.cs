using MongoDB.Bson;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.Services
{
    public class RecipeService
    {
        private readonly RecipeDataAccess recipeDbAccess = new();

        public async Task SaveRecipe(string recipeName, string recipeDescription, ObservableCollection<Ingredient> recipeIngredients)
        {        
                var recipe = new Recipe
                {                   
                    Name = recipeName,
                    Description = recipeDescription,
                    Ingredients = recipeIngredients.ToList()
                };
                await recipeDbAccess.CreateRecipe(recipe);
        }

        public async Task UpdateRecipe(string recipeName, string recipeDescription, ObservableCollection<Ingredient> recipeIngredients, ObjectId id)
        {
            var recipe = new Recipe
            {
                Id = id,
                Name = recipeName,
                Description = recipeDescription,
                Ingredients = recipeIngredients.ToList()
            };
            await recipeDbAccess.UpdateRecipe(recipe);
        }

        public Ingredient AddIngredientToRecipe(string ingredientName, string IngredientCategory, string unitName, double amount)
        {

            var newIngredient = new Ingredient
            {
                Name = ingredientName,
                Category = IngredientCategory,

                Unit = new Unit
                {
                    UnitName = unitName,
                    Amount = amount
                }
            };

            return newIngredient;

        }
    }
}
