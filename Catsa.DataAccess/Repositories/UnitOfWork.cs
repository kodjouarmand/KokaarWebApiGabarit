using Catsa.DataAccess.Contracts;
using System.Threading.Tasks;
using Catsa.DataAccess.Contexts;

namespace Catsa.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatsaDbContext _repositoryContext;
        private IProxyRepository _proxyRepository;
        public UnitOfWork(CatsaDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IProxyRepository Proxy
        {
            get
            {
                if (_proxyRepository == null)
                    _proxyRepository = new ProxyRepository(_repositoryContext);
                return _proxyRepository;
            }
        }
        
        public void Save() => _repositoryContext.SaveChanges();
    }

}
