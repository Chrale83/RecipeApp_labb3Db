using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace RecipeDbAccess.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
