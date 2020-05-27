using Catsa.Domain.Entities;
using System.Collections.Generic;

namespace Catsa.DataAccess.Repositories.Contracts
{
    public interface IProxyRepository : IBaseRepository<Proxy, int>
    {
        public void Update(Proxy proxy);        
    }
}
