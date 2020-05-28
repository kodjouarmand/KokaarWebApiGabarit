using Catsa.BusinessLogic.Queries;
using Catsa.Domain.Assemblers.Proxies;
using System;

namespace Catsa.BusinessLogic.Commands.Proxies
{
    public interface IProxyCommand : IBaseCommand<ProxyCommandDto, Guid>
    {

    }
}