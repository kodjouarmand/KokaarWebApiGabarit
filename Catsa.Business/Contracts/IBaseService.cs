using Catsa.Model.Entities;
using Catsa.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Catsa.Business.Enums;

namespace Catsa.Business.Contracts
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