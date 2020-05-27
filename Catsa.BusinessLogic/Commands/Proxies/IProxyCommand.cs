using Catsa.BusinessLogic.Queries;
using Catsa.Domain.Assemblers.Proxies;

namespace Catsa.BusinessLogic.Commands.Proxies
{
    public interface IProxyCommand : IBaseCommand<ProxyCommandDto, int>
    {

    }
}