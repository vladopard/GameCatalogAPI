using GameCatalogAPI.DTOS;

namespace GameCatalogAPI.Repository
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(UserLoginDTO model);
        Task<AuthenticationResult> RegisterAsync(UserRegistrationDTO model);
    }
}