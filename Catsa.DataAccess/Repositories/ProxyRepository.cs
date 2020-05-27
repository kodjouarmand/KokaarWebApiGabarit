using Catsa.DataAccess.Contracts;
using Catsa.Domain;
using Catsa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catsa.DataAccess.Contexts;

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
