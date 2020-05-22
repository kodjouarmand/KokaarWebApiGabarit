using KokaarWebApiGabarit.Model.DataTransferObjects;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Infrastructure.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }

}
