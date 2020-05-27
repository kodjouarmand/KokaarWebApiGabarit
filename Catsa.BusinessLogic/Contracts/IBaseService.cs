using Catsa.Domain.Entities;
using Catsa.Domain.Assemblers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Catsa.BusinessLogic.Enums;

namespace Catsa.BusinessLogic.Contracts
{
    public interface IBaseService<TBusinessObject> where TBusinessObject : BaseDto
    {
        DataBaseActionEnum DataBaseAction { get; set; }
        string CurrentUser { get; set; }

        TBusinessObject GetById(int id);
        IEnumerable<TBusinessObject> GetAll();
        void Add(TBusinessObject businessObject);
        void Update(TBusinessObject businessObject);
        void Delete(int businessObjectId);       
    }
}