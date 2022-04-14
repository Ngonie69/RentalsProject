using MongoDB.Bson;

namespace RentalsAPI.Models
{
    public class UserDetails
    {
        public ObjectId Id { get; init; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

    }
}
