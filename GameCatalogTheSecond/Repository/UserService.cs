using GameCatalogAPI.DbContexts;
using GameCatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameCatalogAPI.Repository
{
    public class UserService : IUserService
    {
        private readonly GameCatalogContext _context;

        public UserService(GameCatalogContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;
            return null;

        }

        public async Task<bool> RegisterUserAsync(string username,
            string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = Role.User,
                Age = 69
            };

            _context.Users.Add(newUser);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

    }
}
