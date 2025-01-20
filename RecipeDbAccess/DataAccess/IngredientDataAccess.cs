using MongoDB.Bson;
using MongoDB.Driver;
using RecipeDbAccess.Models;
using System.Diagnostics;

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

        public async Task GetAndSetIngredient(Ingredient selectedIngredient)
        {
            try
            {
                var allIngredients = ConnectToMongo<Ingredient>(IngredientCollection);

                bool exists = await allIngredients.Find(_ => _.Name == selectedIngredient.Name).AnyAsync();

                if (!exists)
                {
                    var newIngredient = new Ingredient
                    {
                        Name = selectedIngredient.Name,
                        Category = selectedIngredient.Category,
                    };
                    await CreateIngredient(newIngredient);
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            
     
        }

        public async Task SetAllIngredients(List<Ingredient> ingredients)
        {
            var ingredintCollection = ConnectToMongo<Ingredient>(IngredientCollection);
            await ingredintCollection.InsertManyAsync(ingredients);
        }
    }
}
