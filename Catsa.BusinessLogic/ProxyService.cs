using AutoMapper;
using Catsa.Domain.Entities;
using Catsa.BusinessLogic.Contracts;
using Catsa.BusinessLogic.Validations;
using Catsa.Domain.Assemblers;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.DataAccess.Contracts;
using System.Threading.Tasks;
using Catsa.BusinessLogic.Enums;

namespace Catsa.BusinessLogic
{
    [Serializable()]
    public class ProxyService : BaseService<ProxyDto, Proxy>, IProxyService
    {
        public ProxyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<ProxyDto> GetAll()
        {
            var proxies = _unitOfWork.Proxy.GetAll();
            return MapEntitiesToDto(proxies);
        }

        public override ProxyDto GetById(int proxyId)
        {
            var proxy = _unitOfWork.Proxy.Get(proxyId);
            return MapEntityToDto(proxy);
        }

        protected override StringBuilder ValidateAdd(ProxyDto proxyDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (!proxyDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez ajouter existe déjà.");
                return validationErrors;
            }
            var validationResult = new ProxyValidator().Validate(proxyDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }
       
        public override void Add(ProxyDto proxyDto)
        {
            var proxy = BuildEntity(proxyDto);
            _unitOfWork.Proxy.Add(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateUpdate(ProxyDto proxyDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (proxyDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez mettre à jour n'existe pas.");
                return validationErrors;
            }
            var validationResult = new ProxyValidator().Validate(proxyDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }

        public override void Update(ProxyDto proxyDto)
        {
            var proxy = BuildEntity(proxyDto);            
            _unitOfWork.Proxy.Update(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateDelete(ProxyDto proxyDto = null)
        {
            StringBuilder validationErrors = new StringBuilder();
            if (DataBaseAction != DataBaseActionEnum.Delete)
            {
                validationErrors.Append("DataBaseAction n'est pas mis à Delete.");
                return validationErrors;
            }

            return validationErrors;
        }

        public override void Delete(int proxyId)
        {
            var validationErrors = ValidateDelete();
            if (validationErrors.Length == 0)
            {
                _unitOfWork.Proxy.Delete(proxyId);
                _unitOfWork.Save();
            }
            else
            {
                throw new ValidationException(validationErrors.ToString());
            }
        }

    }
}
