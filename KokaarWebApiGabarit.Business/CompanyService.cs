using AutoMapper;
using KokaarWebApiGabarit.Model.Entities;
using KokaarWepApi.Business.Contracts;
using KokaarWepApi.Business.Validations;
using KokaarWebApiGabarit.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using KokaarWebApiGabarit.Persistance.Contracts;
using System.Threading.Tasks;
using KokaarWebApiGabarit.Business.Enums;

namespace KokaarWepApi.Business
{
    [Serializable()]
    public class CompanyService : BaseService<CompanyDto, Company>, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<CompanyDto> GetAll()
        {
            var companies = _unitOfWork.Company.GetAll();
            return MapEntitiesToDto(companies);
        }

        public override CompanyDto GetById(int companyId)
        {
            var company = _unitOfWork.Company.Get(companyId);
            return MapEntityToDto(company);
        }

        protected override StringBuilder ValidateAdd(CompanyDto companyDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (!companyDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez ajouter existe déjà.");
                return validationErrors;
            }
            var validationResult = new CompanyValidator().Validate(companyDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }
       
        public override void Add(CompanyDto companyDto)
        {
            var company = BuildEntity(companyDto);
            _unitOfWork.Company.Add(company);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateUpdate(CompanyDto companyDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (companyDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez mettre à jour n'existe pas.");
                return validationErrors;
            }
            var validationResult = new CompanyValidator().Validate(companyDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }

        public override void Update(CompanyDto companyDto)
        {
            var company = BuildEntity(companyDto);            
            _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateDelete(CompanyDto companyDto = null)
        {
            StringBuilder validationErrors = new StringBuilder();
            if (DataBaseAction != DataBaseActionEnum.Delete)
            {
                validationErrors.Append("DataBaseAction n'est pas mis à Delete.");
                return validationErrors;
            }

            return validationErrors;
        }

        public override void Delete(int companyId)
        {
            var validationErrors = ValidateDelete();
            if (validationErrors.Length == 0)
            {
                _unitOfWork.Company.Delete(companyId);
                _unitOfWork.Save();
            }
            else
            {
                throw new ValidationException(validationErrors.ToString());
            }
        }

    }
}
