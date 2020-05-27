using Catsa.Domain.Assemblers;
using System.Threading.Tasks;

namespace Catsa.Infrastructure.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }

}
