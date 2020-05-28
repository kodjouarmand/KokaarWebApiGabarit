using Catsa.DataAccess.Contexts;
using Catsa.DataAccess.Repositories.Contracts;
using System;

namespace Catsa.DataAccess.Repositories
{
    public class CatsaDbUnitOfWork : ICatsaDbUnitOfWork
    {
        private CatsaDbContext _catsaDbContext;
        private IProxyRepository _proxyRepository;
        public CatsaDbUnitOfWork(CatsaDbContext catsaDbContext)
        {
            _catsaDbContext = catsaDbContext ?? throw new ArgumentNullException(nameof(catsaDbContext));
        }
        public IProxyRepository Proxy
        {
            get
            {
                if (_proxyRepository == null)
                    _proxyRepository = new ProxyRepository(_catsaDbContext);
                return _proxyRepository;
            }
        }
        
        public void Save() => _catsaDbContext.SaveChanges();
    }

}
