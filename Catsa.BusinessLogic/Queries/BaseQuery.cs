using AutoMapper;
using Catsa.Domain.Entities;
using Catsa.Domain.Assemblers;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.BusinessLogic.Exceptions;
using Catsa.BusinessLogic.Enums;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.BusinessLogic.Queries
{
    [Serializable()]
    public abstract class BaseQuery<TBusinessObject, TEntity, TEntityKey> : IBaseQuery<TBusinessObject, TEntityKey> where TBusinessObject : BaseDto where TEntity : BaseEntity<TEntityKey>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public abstract TBusinessObject GetById(TEntityKey id);
        public abstract IEnumerable<TBusinessObject> GetAll();

        protected TBusinessObject MapEntityToDto(TEntity entity) => _mapper.Map<TBusinessObject>(entity);
        
        protected IEnumerable<TBusinessObject> MapEntitiesToDto(IEnumerable<TEntity> entities) => _mapper.Map<IEnumerable<TBusinessObject>>(entities);
    }
}
