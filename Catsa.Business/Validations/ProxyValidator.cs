using FluentValidation;
using Catsa.Model.DataTransferObjects;

namespace Catsa.Business.Validations
{
    public class ProxyValidator : AbstractValidator<ProxyDto>
    {
        public ProxyValidator()
        {
            RuleFor(proxy => proxy.Nom).NotNull().NotEmpty()
                .WithMessage("The name should not be null or empty");            
        }
    }
}
