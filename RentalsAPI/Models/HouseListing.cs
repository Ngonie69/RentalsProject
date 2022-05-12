
namespace RentalsAPI.Models
{
    public class HouseListing
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string OwnerId { get; set; }
    }
}
