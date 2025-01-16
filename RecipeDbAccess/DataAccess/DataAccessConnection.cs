using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDbAccess.DataAccess
{
    public abstract class DataAccessConnection
    {
        private const string connectionString = "mongodb://localhost:27017";
        private const string dataBase = "christian_alexandersson";

        public IMongoCollection<T> ConnectToMongo<T>(in string collectionName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dataBase);
            return db.GetCollection<T>(collectionName);
        }
    }
}
