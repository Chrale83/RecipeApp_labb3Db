using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.Services
{
    public class RecipeService
    {
        private readonly RecipeDataAccess recipeDbAccess = new();

        public async Task SaveRecipe(string recipeName, string recipeDescription, ObservableCollection<Ingredient> recipeIngredient)
        {

            var recipe = new Recipe
            {
                Name = recipeName,
                Description = recipeDescription,
                Ingredients = recipeIngredient.ToList()

            };

            await recipeDbAccess.CreateRecipe(recipe);
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
