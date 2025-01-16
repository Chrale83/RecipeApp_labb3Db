using MongoDB.Driver;
using RecipeDbAccess.Models;

namespace RecipeDbAccess.DataAccess
{
    public class IngredientDataAccess : DataAccessConnection
    {
       
        private const string IngredientCollection = "ingredient";
        public async Task CreateIngredient(Ingredient ingredient)
        {
            var ingredientCollection = ConnectToMongo<Ingredient>(IngredientCollection);
            await ingredientCollection.InsertOneAsync(ingredient);
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            var allIngredients = ConnectToMongo<Ingredient>(IngredientCollection);
            var result = await allIngredients.FindAsync(_ => true);
            return result.ToList();
        }
    }
}