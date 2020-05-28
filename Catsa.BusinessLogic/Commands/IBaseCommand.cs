using Catsa.Domain.Assemblers;
using System.Collections.Generic;
using Catsa.BusinessLogic.Enums;

namespace Catsa.BusinessLogic.Commands
{
    public interface IBaseCommand<TBusinessObject, TEntityKey> where TBusinessObject : BaseCommandDto<TEntityKey>
    {
        DataBaseActionEnum DataBaseAction { get; set; }
        string CurrentUser { get; set; }

        TEntityKey Add(TBusinessObject businessObject);
        void Update(TBusinessObject businessObject);
        void Delete(TEntityKey businessObjectId);
        void Save();
    }
}