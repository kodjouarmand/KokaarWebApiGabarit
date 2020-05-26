using KokaarWebApiGabarit.Persistance.Contracts;
using KokaarWebApiGabarit.Model;
using KokaarWebApiGabarit.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokaarWebApiGabarit.Persistance.Data;

namespace KokaarWebApiGabarit.Persistance.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges = false) =>
            GetByCondition(x => ids.Contains(x.Id), trackChanges).ToList();       

    }

}
