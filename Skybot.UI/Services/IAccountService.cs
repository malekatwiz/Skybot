using System.Threading.Tasks;
using Skybot.UI.Models;

namespace Skybot.UI.Services
{
    public interface IAccountService
    {
        Task<bool> HasAccount(string phoneNumber);
        Task SendAccessCode(string phoneNumber);
        Task<bool> Create(UserAccountModel userAccountModel);
        Task<bool> ValidateAccessCode(VerificationCodeModel verificationCodeModel);
    }
}
