using AutoMapper;
using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.BusinessLogic.Queries.Proxies
{
    public class ProxyQuery : BaseQuery<ProxyQueryDto, Proxy, Guid>, IProxyQuery
    {
        public ProxyQuery(ICatsaDbUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<ProxyQueryDto> GetAll()
        {
            var proxies = _unitOfWork.Proxy.GetAll();
            return MapEntitiesToDto(proxies);
        }

        public override ProxyQueryDto GetById(Guid proxyId)
        {
            var proxy = _unitOfWork.Proxy.GetById(proxyId);
            return MapEntityToDto(proxy);
        }
    }
}
