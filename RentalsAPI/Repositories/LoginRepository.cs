using MongoDB.Bson;
using MongoDB.Driver;
using RentalsAPI.DTO;
using RentalsAPI.Models;

namespace RentalsAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private const string database = "Rentals";

        private const string collectionName = "login";

        private readonly IMongoCollection<UserDetails> loginCollection;

        private readonly FilterDefinitionBuilder<UserDetails> filterBuilder = Builders<UserDetails>.Filter;

        public LoginRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(database);
            loginCollection = mongoDatabase.GetCollection<UserDetails>(collectionName);
        }
    }
}