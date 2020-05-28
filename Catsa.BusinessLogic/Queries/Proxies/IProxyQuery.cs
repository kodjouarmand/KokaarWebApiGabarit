using Catsa.BusinessLogic.Queries;
using Catsa.Domain.Assemblers.Proxies;
using System;

namespace Catsa.BusinessLogic.Queries.Proxies
{
    public interface IProxyQuery : IBaseQuery<ProxyQueryDto, Guid>
    {

    }
}