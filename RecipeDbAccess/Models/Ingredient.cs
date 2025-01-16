using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeDbAccess.Models
{
    public class Ingredient
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        public string Category { get; set; }
        public Unit? Unit { get; set; }

        
    }
}
