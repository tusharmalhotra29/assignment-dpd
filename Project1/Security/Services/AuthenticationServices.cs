using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Project1.Database;
using Project1.Models;

namespace Project1.Security.Services
{
    public class AuthenticationServices
    {
        private readonly DataContext _context;

        public AuthenticationServices(DataContext context)
        {
            _context = context;
        }
        public User? Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            byte[] salt = Convert.FromBase64String(user.Password);

            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            );

            if (Convert.ToBase64String(hashedPassword) != user.Password)
            {
                return null;
            }

            return user;
        }
    }

}
