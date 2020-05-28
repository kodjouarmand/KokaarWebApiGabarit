using AutoMapper;
using Catsa.Domain.Entities;
using Catsa.Domain.Assemblers;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.BusinessLogic.Exceptions;
using Catsa.BusinessLogic.Enums;
using Catsa.DataAccess.Repositories.Contracts;
using Catsa.BusinessLogic.Queries;

namespace Catsa.BusinessLogic.Commands
{
   public abstract class BaseCommand<TBusinessObject, TEntity, TEntityKey> : IBaseCommand<TBusinessObject, TEntityKey> where TBusinessObject : BaseCommandDto<TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        protected readonly ICatsaDbUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public DataBaseActionEnum DataBaseAction { get; set; }
        public string CurrentUser { get; set; }

        public BaseCommand(ICatsaDbUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            DataBaseAction = DataBaseActionEnum.Save;
        }

        public abstract TEntityKey Add(TBusinessObject businessObject);
        public abstract void Update(TBusinessObject businessObject);
        public abstract void Delete(TEntityKey businessObjectId);
        public abstract void Save();

        protected TEntity BuildEntity(TBusinessObject businessObject)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentUser))
            {
                validationErrors.Append("L'utilisateur qui effectue l'opération est requis.");
                throw new CommandValidationException(validationErrors.ToString());
            }
            if (DataBaseAction != DataBaseActionEnum.Save)
            {
                validationErrors.Append("DataBaseAction n'est pas mis à Save.");
                throw new CommandValidationException(validationErrors.ToString());
            }
            if (businessObject.IsNew())
            {
                validationErrors = ValidateAdd(businessObject);
            }
            else
            {
                validationErrors = ValidateUpdate(businessObject);
            }

            if (validationErrors.Length != 0)
            {
                throw new CommandValidationException(validationErrors.ToString());
            }

            TEntity entity = MapDtoToEntity(businessObject);
            return entity;
        }

        protected abstract StringBuilder ValidateAdd(TBusinessObject businessObject);

        protected abstract StringBuilder ValidateUpdate(TBusinessObject businessObject);

        protected abstract StringBuilder ValidateDelete(TBusinessObject businessObject);

        protected TEntity MapDtoToEntity(TBusinessObject businessObject)
        {
            TEntity entity = _mapper.Map<TEntity>(businessObject);

            if (businessObject.IsNew())
            {
                entity.CreationDate = DateTime.Now;
                entity.CreationUser = CurrentUser;
            }
            else
            {                
                entity.LastModificationDate = DateTime.Now;
                entity.LastModificationUser = CurrentUser;
            }

            return entity;
        }
    }
}
