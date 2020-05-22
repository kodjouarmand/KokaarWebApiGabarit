using KokaarWebApiGabarit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(int companyId, bool trackChanges);
        void CreateCompany(Company company);
        IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void DeleteCompany(Company company);

        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(int companyId, bool trackChanges);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    }
}
