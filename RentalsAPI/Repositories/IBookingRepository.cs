using RentalsAPI.Models;

namespace RentalsAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingModel> GetBooking(Guid Id);
        Task<List<BookingModel>> GetAllBookings();
        Task<BookingModel> AddBooking(BookingModel booking);
        Task UpdateBooking(BookingModel booking);
        Task DeleteBooking(Guid Id);
    }
}