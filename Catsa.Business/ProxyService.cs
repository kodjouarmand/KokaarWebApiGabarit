using AutoMapper;
using Catsa.Model.Entities;
using Catsa.Business.Contracts;
using Catsa.Business.Validations;
using Catsa.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.Persistance.Contracts;
using System.Threading.Tasks;
using Catsa.Business.Enums;

namespace Catsa.Business
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
