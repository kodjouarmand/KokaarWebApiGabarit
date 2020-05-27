using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catsa.DataAccess.Repositories.Contracts
{
    public interface IProxyRepository : IBaseRepository<Proxy, int>
    {
        IEnumerable<Proxy> GetByIds(IEnumerable<int> ids, bool trackChanges = true);

        //public void Update(Proxy proxy);        
    }
}
