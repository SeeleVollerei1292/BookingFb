using bookingfootball.Common.Reponse;
using bookingfootball.Common.Request;

namespace bookingfootball.Constract
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string email, string password);
        Task<Token> RefreshTokenAsync(string refreshToken);
        Task<Token> RegisterAsync(RegisterRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync();
    }
}
