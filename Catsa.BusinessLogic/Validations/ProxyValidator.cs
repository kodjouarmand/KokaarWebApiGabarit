using FluentValidation;
using Catsa.Domain.Assemblers;

namespace Catsa.BusinessLogic.Validations
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
