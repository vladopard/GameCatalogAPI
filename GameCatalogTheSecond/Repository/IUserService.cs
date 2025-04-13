using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repository
{
    public interface IUserService
    {
        Task<User?> AuthenticateUserAsync(string username, string password);
        Task<bool> RegisterUserAsync(string username, string password);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}