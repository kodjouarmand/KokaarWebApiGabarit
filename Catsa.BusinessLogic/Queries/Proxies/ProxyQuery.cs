using AutoMapper;
using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.BusinessLogic.Queries.Proxies
{
    [Serializable()]
    public class ProxyQuery : BaseQuery<ProxyQueryDto, Proxy, int>, IProxyQuery
    {
        public ProxyQuery(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<ProxyQueryDto> GetAll()
        {
            var proxies = _unitOfWork.Proxy.GetAll();
            return MapEntitiesToDto(proxies);
        }

        public override ProxyQueryDto GetById(int proxyId)
        {
            var proxy = _unitOfWork.Proxy.Get(proxyId);
            return MapEntityToDto(proxy);
        }
    }
}
