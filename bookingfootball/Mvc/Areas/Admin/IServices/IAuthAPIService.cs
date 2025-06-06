using Mvc.Models;
using Mvc.Request;

namespace Mvc.Areas.Admin.IServices
{
    public interface IAuthAPIService
    {
        Task<Token> LoginAsync(LoginRequest request);
        Task<Token> RegisterAsync(RegisterRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync();
    }
}
