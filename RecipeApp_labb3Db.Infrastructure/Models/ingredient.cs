using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeApp_labb3Db.Infrastructure.Models
{

    public class ingredient
    {

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; } 

        public string Name { get; set; }
        public string Category { get; set; }

    }
}
    