using MongoDB.Bson;
using MongoDB.Driver;
using RentalsAPI.Models;
using RentalsAPI.Repositories;

namespace RentalsAPI.Repositories
{
   public class BookingRepository : IBookingRepository
   {
        private const string database = "Rentals";
        private const string collectionName = "Booking";
        private readonly IMongoCollection<BookingModel> bookingCollection;
        private readonly FilterDefinitionBuilder<BookingModel> filterBuilder = Builders<BookingModel>.Filter;

        public BookingRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(database);
            bookingCollection = mongoDatabase.GetCollection<BookingModel>(collectionName);
        }

        async Task<BookingModel> IBookingRepository.AddBooking(BookingModel booking)
        {
            await bookingCollection.InsertOneAsync(booking);
            return booking;
        }

        Task IBookingRepository.DeleteBooking(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return bookingCollection.DeleteOneAsync(filter);
        }

        Task<List<BookingModel>> IBookingRepository.GetAllBookings()
        {
            return bookingCollection.Find(new BsonDocument()).ToListAsync();
        }

        Task<BookingModel> IBookingRepository.GetBooking(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return bookingCollection.Find(filter).FirstOrDefaultAsync();
        }

        Task IBookingRepository.UpdateBooking(BookingModel booking)
        {
            var filter = filterBuilder.Eq(item => item.Id, booking.Id);
            return bookingCollection.ReplaceOneAsync(filter, booking);
        }
    }
}