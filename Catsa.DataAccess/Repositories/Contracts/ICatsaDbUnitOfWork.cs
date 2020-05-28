using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catsa.DataAccess.Repositories.Contracts
{
    public interface ICatsaDbUnitOfWork
    {
        IProxyRepository Proxy { get; }

        void Save();
    }

}
