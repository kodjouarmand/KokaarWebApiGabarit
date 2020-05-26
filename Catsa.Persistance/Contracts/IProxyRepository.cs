using Catsa.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catsa.Persistance.Contracts
{
    public interface IProxyRepository : IBaseRepository<Proxy>
    {
        IEnumerable<Proxy> GetByIds(IEnumerable<int> ids, bool trackChanges = false);
    }
}
