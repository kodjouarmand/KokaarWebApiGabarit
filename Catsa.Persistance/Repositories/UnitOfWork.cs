using Catsa.Persistance.Contracts;
using System.Threading.Tasks;
using Catsa.Persistance.Data;

namespace Catsa.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _repositoryContext;
        private IProxyRepository _proxyRepository;
        public UnitOfWork(ApplicationDbContext repositoryContext)
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
