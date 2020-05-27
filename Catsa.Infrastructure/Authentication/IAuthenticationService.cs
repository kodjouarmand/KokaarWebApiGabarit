using Catsa.Domain.Assemblers.Users;
using System.Threading.Tasks;

namespace Catsa.Infrastructure.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }

}
