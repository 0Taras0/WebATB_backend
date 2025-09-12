using Core.Constants;
using Core.Models.Account;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResult> RegisterAsync(RegisterModel model);
    }
}
