using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalsAPI.DTO;
using RentalsAPI.Models;
using RentalsAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Xamarin.Essentials;

namespace RentalsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserDetails user = new UserDetails();
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        public AuthController(IConfiguration configuration, ILoginRepository loginRepository)
        {
            _config = configuration;
            _loginRepository = loginRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDetails>> Register(UserDTO request)
        {
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                UserDetails user = new UserDetails
                {
                    Email = request.Email,
                    Username = request.Username,
                    Password = request.Password,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var check = await _loginRepository.ListOfUsersAsync();
                foreach (var _user in check)
                {
                    if (_user.Email == request.Email)
                    {
                        return BadRequest("The user already exists");
                    }
                }

                await _loginRepository.RegisterNewUserAsync(user);

                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDetails>> Login(UserDTO login)
        {
            try
            {
                var _login = await _loginRepository.LoginAsync(login);

                if (_login.Email != login.Email)
                {
                    return BadRequest("User does not exist");
                }

                CreatePasswordHash(login.Password, out byte[] passwordHash, out byte[] passwordSalt);

                if (!VerifyPasswordHash(login.Password, passwordHash, passwordSalt))
                {
                    return BadRequest("Incorrect credentials");
                }
                string token = CreateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult<ForgotPasswordDTO>> ForgotPassword(ForgotPasswordDTO user)
        {
            var result = await _loginRepository.ForgotPassword(user);
            if (result == null)
            {
                return BadRequest("The supplied email isn't registered");
            }
            Preferences.Set("Email", user.Email);
            return Ok();
        }


        private string CreateToken(UserDetails user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
              
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
