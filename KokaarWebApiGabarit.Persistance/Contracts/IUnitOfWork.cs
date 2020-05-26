using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Contracts
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }

        void Save();
    }

}
