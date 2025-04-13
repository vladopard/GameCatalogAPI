using GameCatalogAPI.DTOS;

namespace GameCatalogAPI.Repository
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthenticationService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResult> LoginAsync(UserLoginDTO model)
        {
            var user = await _userService.AuthenticateUserAsync(model.Username, model.Password);
            if (user == null)
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Invalid Credentials"
                };

            var token = _jwtService.GenerateToken(user);
            return new AuthenticationResult
            {
                Success = true,
                Message = "Here s the token boi",
                Token = token
            };
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationDTO model)
        {
            var registered = await _userService.RegisterUserAsync
                (model.Username, model.Password);

            if (!registered)
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Username already exists"
                };

            return new AuthenticationResult
            {
                Success = true,
                Message = "User registered successfully"
            };
        }
    }
}
