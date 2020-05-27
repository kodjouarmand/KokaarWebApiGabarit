using AutoMapper;
using Catsa.Domain.Entities;
using Catsa.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.BusinessLogic.Enums;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.BusinessLogic.Commands.Proxies
{
    [Serializable()]
    public class ProxyCommand : BaseCommand<ProxyCommandDto, Proxy, int>, IProxyCommand
    {
        public ProxyCommand(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected override StringBuilder ValidateAdd(ProxyCommandDto proxyDto)
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
       
        public override void Add(ProxyCommandDto proxyDto)
        {
            var proxy = BuildEntity(proxyDto);
            _unitOfWork.Proxy.Add(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateUpdate(ProxyCommandDto proxyDto)
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

        public override void Update(ProxyCommandDto proxyDto)
        {
            var proxy = BuildEntity(proxyDto);            
            _unitOfWork.Proxy.Update(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateDelete(ProxyCommandDto proxyDto = null)
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
                throw new CommandValidationException(validationErrors.ToString());
            }
        }

    }
}
