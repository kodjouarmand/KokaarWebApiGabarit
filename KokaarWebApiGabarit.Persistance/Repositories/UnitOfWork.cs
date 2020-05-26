using KokaarWebApiGabarit.Persistance.Contracts;
using System.Threading.Tasks;
using KokaarWebApiGabarit.Persistance.Data;

namespace KokaarWebApiGabarit.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        public UnitOfWork(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        
        public void Save() => _repositoryContext.SaveChanges();
    }

}
