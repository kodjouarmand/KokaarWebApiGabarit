using Catsa.Persistance.Contracts;
using Catsa.Model;
using Catsa.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catsa.Persistance.Data;

namespace Catsa.Persistance.Repositories
{
    public class ProxyRepository : BaseRepository<Proxy>, IProxyRepository
    {
        public ProxyRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IEnumerable<Proxy> GetByIds(IEnumerable<int> ids, bool trackChanges = false) =>
            GetByCondition(x => ids.Contains(x.Id), trackChanges).ToList();       

    }

}
