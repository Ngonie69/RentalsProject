using MongoDB.Bson;
using MongoDB.Driver;
using RentalsAPI.DTO;
using RentalsAPI.Models;

namespace RentalsAPI.Repositories
{
    public class HouseListingRepository : IHouseListingRepository
    {
        private const string database = "Rentals";

        private const string collectionName = "houseListing";

        private readonly IMongoCollection<HouseListing> houseListingCollection;

        private readonly FilterDefinitionBuilder<HouseListing> filterBuilder = Builders<HouseListing>.Filter;

        public HouseListingRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(database);
            houseListingCollection = mongoDatabase.GetCollection<HouseListing>(collectionName);
        }

        public async Task DeleteHouseListing(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await houseListingCollection.DeleteOneAsync(filter);
        }

        async Task<HouseListing> IHouseListingRepository.AddHouseListing(HouseListing houseListing)
        {
            await houseListingCollection.InsertOneAsync(houseListing);
            return houseListing;
        }

        Task<List<HouseListing>> IHouseListingRepository.GetAllHouseListings()
        {
            return houseListingCollection.Find(new BsonDocument()).ToListAsync();
        }

        Task<HouseListing> IHouseListingRepository.GetHouseListing(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return houseListingCollection.Find(filter).FirstOrDefaultAsync();
        }      

        async Task IHouseListingRepository.UpdateHouseListing(HouseListing houseListing)
        {
            var filter = filterBuilder.Eq(item => item.Id, houseListing.Id);
            await houseListingCollection.ReplaceOneAsync(filter, houseListing);
        }
    }
}