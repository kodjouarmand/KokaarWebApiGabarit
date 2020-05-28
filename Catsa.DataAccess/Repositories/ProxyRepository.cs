using Catsa.Domain.Entities;
using System;
using Catsa.DataAccess.Contexts;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.DataAccess.Repositories
{
    public class ProxyRepository : BaseRepository<Proxy, Guid>, IProxyRepository
    {
        public ProxyRepository(CatsaDbContext catsaDbContext) : base(catsaDbContext) 
        { 
        }

        public virtual void Update(Proxy proxyToUpdate)
        {
            var originalEntity = GetById(proxyToUpdate.Id);

            if (!string.IsNullOrWhiteSpace(proxyToUpdate.Nom)) originalEntity.Nom = proxyToUpdate.Nom;
            if (!string.IsNullOrWhiteSpace(proxyToUpdate.Type)) originalEntity.Type = proxyToUpdate.Type;
            if (!string.IsNullOrWhiteSpace(proxyToUpdate.Description)) originalEntity.Description = proxyToUpdate.Description;
            originalEntity.LastModificationDate = proxyToUpdate.LastModificationDate;
            originalEntity.LastModificationUser = proxyToUpdate.LastModificationUser;

            dbSet.Update(originalEntity);
        }
    }

}
