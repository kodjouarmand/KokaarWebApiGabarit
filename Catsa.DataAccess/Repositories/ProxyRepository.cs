using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Catsa.DataAccess.Contexts;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.DataAccess.Repositories
{
    public class ProxyRepository : BaseRepository<Proxy, int>, IProxyRepository
    {
        public ProxyRepository(CatsaDbContext catsaDbContext) : base(catsaDbContext) { }

        public IEnumerable<Proxy> GetByIds(IEnumerable<int> ids, bool trackChanges = true) =>
            GetByCondition(x => ids.Contains(x.Id), trackChanges).ToList();

        //public virtual void Update(Proxy entity)
        //{
            //var originalEntity = GetById(entity.Id, true);
            //if (string.IsNullOrWhiteSpace(entity.Nom)) originalEntity.Nom = entity.Nom;
            //if (string.IsNullOrWhiteSpace(entity.Type)) originalEntity.Type = entity.Type;
            //if (string.IsNullOrWhiteSpace(entity.Description)) originalEntity.Description = entity.Description;
            //originalEntity.LastModificationDate = entity.LastModificationDate;
            //originalEntity.LastModificationUser = entity.LastModificationUser;       
        //}
    }

}
