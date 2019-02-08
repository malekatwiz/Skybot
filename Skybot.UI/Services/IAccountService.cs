using System.Threading.Tasks;
using Skybot.UI.Models;

namespace Skybot.UI.Services
{
    public interface IAccountService
    {
        Task<bool> HasAccountAsync(string phoneNumber);
        Task SendAccessCodeAsync(string phoneNumber);
        Task<bool> CreateAsync(UserAccountModel userAccountModel);
        Task<UserAccountModel> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> ValidateAccessCodeAsync(VerificationCodeModel verificationCodeModel);
    }
}
