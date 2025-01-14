using Microsoft.EntityFrameworkCore;

namespace RecipeApp_labb3Db.Infrastructure.Models
{
    public class RecipeAppContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = "mongodb://localhost:27017/<database>";
            var dataBase = "CookBook";


            optionsBuilder.UseMongoDB(connectionstring, dataBase);


        }

    }
}
