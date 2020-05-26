using AutoMapper;
using Catsa.Model.Entities;
using Catsa.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.Persistance.Contracts;
using Catsa.Business.Validations;
using Catsa.Business.Enums;

namespace Catsa.Business.Contracts
{    
    [Serializable()]
    public abstract class BaseService<TBusinessObject, TEntity> : IBaseService<TBusinessObject> where TBusinessObject : BaseDto where TEntity : BaseEntity<int>
    {       
        #region Constructor

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            DataBaseAction = DataBaseActionEnum.Save;
        }

        #endregion Constructor

        #region Properties

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public DataBaseActionEnum DataBaseAction { get; set; }
        public string CurrentUser { get; set; }

        #endregion Properties

        #region Abstract Methods               

        public abstract TBusinessObject GetById(int id);
        public abstract IEnumerable<TBusinessObject> GetAll();

        public abstract void Add(TBusinessObject businessObject);

        public abstract void Update(TBusinessObject businessObject);

        public abstract void Delete(int businessObjectId);

        protected TEntity BuildEntity(TBusinessObject businessObject)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentUser))
            {
                validationErrors.Append("L'utilisateur qui effectue l'opération est requis.");                
                throw new ValidationException(validationErrors.ToString());
            }
            if (DataBaseAction != DataBaseActionEnum.Save)
            {
                validationErrors.Append("DataBaseAction n'est pas mis à Save.");
                throw new ValidationException(validationErrors.ToString());
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
                throw new ValidationException(validationErrors.ToString());                
            }

            TEntity entity = MapDtoToEntity(businessObject);
            return entity;
        }

        protected abstract StringBuilder ValidateAdd(TBusinessObject businessObject);

        protected abstract StringBuilder ValidateUpdate(TBusinessObject businessObject);

        protected abstract StringBuilder ValidateDelete(TBusinessObject businessObject);

        #endregion Abstratct Methods

        #region Public Methods

        protected TBusinessObject MapEntityToDto(TEntity entity) => _mapper.Map<TBusinessObject>(entity);
        
        protected IEnumerable<TBusinessObject> MapEntitiesToDto(IEnumerable<TEntity> entities) => _mapper.Map<IEnumerable<TBusinessObject>>(entities);


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
                var originalBusinessObject = GetById(businessObject.Id);
                entity.CreationDate = originalBusinessObject.CreationDate;
                entity.CreationUser = originalBusinessObject.CreationUser;
                entity.LastModificationDate = DateTime.Now;
                entity.LastModificationUser = CurrentUser;
            }

            return entity;
        }

        #endregion Public Methods
    }
}
