using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catsa.Persistance.Contracts
{
    public interface IUnitOfWork
    {
        IProxyRepository Proxy { get; }

        void Save();
    }

}
