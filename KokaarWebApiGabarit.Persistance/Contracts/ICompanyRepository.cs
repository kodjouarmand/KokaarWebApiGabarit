using KokaarWebApiGabarit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Contracts
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges = false);
    }
}
