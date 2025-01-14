namespace RecipeApp_labb3Db.Infrastructure.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ingredient> Ingredients { get; set; }

    }
}
