using RentalsAPI.DTO;
using RentalsAPI.Models;

namespace RentalsAPI.Repositories
{
    public interface ILoginRepository
    {
        Task<UserDetails> RegisterNewUserAsync(UserDetails user);
        Task<UserDetails> LoginAsync(UserDTO user);
        Task<IEnumerable<UserDetails>> ListOfUsersAsync();

        Task<UserDetails> ForgotPassword(ForgotPasswordDTO email);
    }
}
