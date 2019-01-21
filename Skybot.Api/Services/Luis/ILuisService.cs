using Skybot.Api.Models;
using System.Threading.Tasks;

namespace Skybot.Api.Services.Luis
{
    public interface ILuisService
    {
        Task<LuisResultModel> Query(string message);
    }
}
