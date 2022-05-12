using RentalsAPI.Models;

namespace RentalsAPI.Repositories
{
    public interface IHouseListingRepository
    {
        public Task<List<HouseListing>> GetAllHouseListings();
        public Task<HouseListing> GetHouseListing(HouseListing houseListing);
        public Task<HouseListing> AddHouseListing(HouseListing houseListing);
        public Task UpdateHouseListing(HouseListing houseListing);
        public Task DeleteHouseListing(Guid Id);       
    }
}
