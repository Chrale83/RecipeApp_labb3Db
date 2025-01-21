﻿using MongoDB.Driver;
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

