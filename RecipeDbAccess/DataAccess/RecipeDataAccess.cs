using MongoDB.Bson;
using MongoDB.Driver;
using RecipeDbAccess.Models;

namespace RecipeDbAccess.DataAccess
{
    public class RecipeDataAccess : DataAccessConnection
    {

        private const string RecipeCollection = "recipes";
        public async Task CreateRecipe(Recipe recipe)
        {
            try
            {
                var recipeDB = ConnectToMongo<Recipe>(RecipeCollection);
                await recipeDB.InsertOneAsync(recipe);
            }
            catch (Exception e)
            {

                throw new Exception("programmet kom inte åt databaseb", e);
            }
        }
        public async Task UpdateRecipe(Recipe recipe)
        {
            var recipeCollection = ConnectToMongo<Recipe>(RecipeCollection);
            var filter = Builders<Recipe>.Filter.Eq(r => r.Id, recipe.Id);
            await recipeCollection.ReplaceOneAsync(filter, recipe);

        }
        public async Task DeleteRecipeFromDb(Recipe recipe)
        {
            var recipeCollection = ConnectToMongo<Recipe>(RecipeCollection);
            await recipeCollection.DeleteOneAsync(r => r.Id == recipe.Id);
        }
        public async Task<List<Recipe>> GetAllRecipes()
        {
            try
            {
                var recipesCollection = ConnectToMongo<Recipe>(RecipeCollection);
                var result = await recipesCollection.FindAsync(_ => true);
                var recipes = await result.ToListAsync();
                return recipes;
            }
            catch (Exception e)
            {

                throw new Exception("programmet kom inte åt databasen", e);
            }
        }

    }
}

