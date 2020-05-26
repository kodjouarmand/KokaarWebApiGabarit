using FluentValidation;
using KokaarWebApiGabarit.Model.DataTransferObjects;

namespace KokaarWepApi.Business.Validations
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.Name).NotNull().NotEmpty()
                .WithMessage("The name should not be null or empty");            
        }
    }
}
