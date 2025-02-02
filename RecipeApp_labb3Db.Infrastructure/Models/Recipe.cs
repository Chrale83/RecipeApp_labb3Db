﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeApp_labb3Db.Infrastructure.Models
{
    
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        
        public List<ingredient> ingredients { get; set; } 
    }
}
