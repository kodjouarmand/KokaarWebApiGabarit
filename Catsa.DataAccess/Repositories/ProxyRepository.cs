using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Catsa.DataAccess.Contexts;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.DataAccess.Repositories
{
    public class ProxyRepository : BaseRepository<Proxy>, IProxyRepository
    {
        public ProxyRepository(CatsaDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IEnumerable<Proxy> GetByIds(IEnumerable<int> ids, bool trackChanges = false) =>
            GetByCondition(x => ids.Contains(x.Id), trackChanges).ToList();       

    }

}
