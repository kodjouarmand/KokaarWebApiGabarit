using Catsa.Domain.Assemblers;
using System.Collections.Generic;
using Catsa.BusinessLogic.Enums;

namespace Catsa.BusinessLogic.Queries
{
    public interface IBaseQuery<TBusinessObject, TEntityKey> where TBusinessObject : BaseDto
    {
        TBusinessObject GetById(TEntityKey id);
        IEnumerable<TBusinessObject> GetAll();        
    }
}