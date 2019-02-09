using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Skybot.UI.Models;

namespace Skybot.UI.Services
{
    public interface IAuthorizationService
    {
        Task<string> GetTokenAsync();
        Task UserSignInAsync(HttpContext httpContext, UserAccountModel userAccount);
        Task UserSignOutAsync(HttpContext httpContext);
    }
}
