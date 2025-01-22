using MongoDB.Driver;
using RecipeDbAccess.Models;

namespace RecipeDbAccess.DataAccess
{
    public class IngredientDataAccess : DataAccessConnection
    {

        private const string IngredientCollection = "ingredient";
        public async Task CreateIngredientToDb(Ingredient ingredient)
        {
            try
            {
                var ingredientCollection = ConnectToMongo<Ingredient>(IngredientCollection);
                await ingredientCollection.InsertOneAsync(ingredient);
            }
            catch (Exception e)
            {

                throw new Exception("Programmet kommer inte åt databasen", e);
            }
        }

        public async Task UpdateIngredientToDb(Ingredient ingredient)
        {
            var ingredientCollection = ConnectToMongo<Ingredient>(IngredientCollection);
            var filter = Builders<Ingredient>.Filter.Eq(ic => ic.Id, ingredient.Id);
            await ingredientCollection.ReplaceOneAsync(filter,ingredient);
            
        }
        public async Task<List<Ingredient>> GetAllIngredientsFromDb()
        {
            try
            {
                var allIngredients = ConnectToMongo<Ingredient>(IngredientCollection);
                var result = await allIngredients.FindAsync(_ => true);
                return result.ToList();

            }
            catch (Exception e)
            {

                throw new Exception("Programmet kom inte åt databasen", e);
            }
        }

        public async Task<bool> GetAndSetIngredientFromDB(Ingredient selectedIngredient)
        {
            try
            {
                var allIngredients = ConnectToMongo<Ingredient>(IngredientCollection);

                bool exists = await allIngredients.Find(i => i.Name == selectedIngredient.Name).AnyAsync();

                if (!exists)
                {
                    var newIngredient = new Ingredient
                    {
                        Name = selectedIngredient.Name.ToLower(),
                        Category = selectedIngredient.Category.ToLower(),
                    };
                    await CreateIngredientToDb(newIngredient);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Programmet kom inte åt databasen.", e);
            }
        }

        public async Task SetAllIngredientsToDB(List<Ingredient> ingredients)
        {
            try
            {
                var ingredintCollection = ConnectToMongo<Ingredient>(IngredientCollection);
                await ingredintCollection.InsertManyAsync(ingredients);
            }
            catch (Exception e)
            {
                throw new Exception("Programmet kom inte åt databasen.", e);
            }
        }

        public async Task<bool> DeleteIngredientFromDb(string ingredientName)
        {
            try
            {
                var ingredientCollection = ConnectToMongo<Ingredient>(IngredientCollection);
                var result = await ingredientCollection.DeleteOneAsync(i => i.Name.ToLower() == ingredientName.ToLower());
                return result.DeletedCount == 1;
            }
            catch (Exception e)
            {
                throw new Exception("Programmet kom inte åt databasen", e);
                
            }
        }
    }
}



