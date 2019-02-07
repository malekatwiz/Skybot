using System.Threading.Tasks;

namespace Skybot.UI.Services
{
    public interface IAuthorizationService
    {
        Task<string> GetToken();
    }
}
