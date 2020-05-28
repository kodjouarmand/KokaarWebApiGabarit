using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Catsa.DataAccess.Repositories.Contracts
{
    public interface IProxyRepository : IBaseRepository<Proxy, Guid>
    {
        public void Update(Proxy proxy);        
    }
}
