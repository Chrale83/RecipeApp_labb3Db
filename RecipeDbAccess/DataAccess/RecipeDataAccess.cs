using MongoDB.Driver;
using RecipeDbAccess.Models;

namespace RecipeDbAccess.DataAccess
{
    public class RecipeDataAccess : DataAccessConnection
    {

        private const string RecipeCollection = "recipes";
        public async Task CreateRecipe(Recipe recipe)
        {
            var recipeDB = ConnectToMongo<Recipe>(RecipeCollection);
            await recipeDB.InsertOneAsync(recipe);
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            var recipesCollection = ConnectToMongo<Recipe>(RecipeCollection);
            var result = await recipesCollection.FindAsync(_ => true);
            var recipes = await result.ToListAsync();
            return recipes;
        }

    }
}
