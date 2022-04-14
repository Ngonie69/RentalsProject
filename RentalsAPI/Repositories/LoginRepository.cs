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
        
        public async Task<UserDetails> ForgotPassword(ForgotPasswordDTO user)
        {
            var filter = filterBuilder.Eq(user => user.Email, user.Email);
            return await loginCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDetails>> ListOfUsersAsync()
        {
            return await loginCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UserDetails> LoginAsync(UserDTO user)
        {
            var filter = filterBuilder.Eq(user => user.Email, user.Email);
            return await loginCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<UserDetails> RegisterNewUserAsync(UserDetails user)
        {
            await loginCollection.InsertOneAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(UserDTO user)
        {
            var filter = filterBuilder.Eq(user => user.Email, user.Email);
            await loginCollection.DeleteOneAsync(filter);
        }
    }
}
