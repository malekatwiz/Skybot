using System.Threading.Tasks;
using Skybot.UI.Models;

namespace Skybot.UI.Services
{
    public interface ISkybotService
    {
        Task<string> SendQueryAsync(SkybotQueryModel model);
    }
}
