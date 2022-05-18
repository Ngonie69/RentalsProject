namespace RentalsAPI.Models
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public Guid HouseListingId { get; set; }
        public string? UserId { get; set; }
        public DateTime BookingDate { get; set; }      
    }
}