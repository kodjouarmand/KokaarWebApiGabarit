using FluentValidation;
using Catsa.Domain.Assemblers.Proxies;

namespace Catsa.BusinessLogic.Commands.Proxies
{
    public class ProxyValidator : AbstractValidator<ProxyCommandDto>
    {
        public ProxyValidator()
        {
            RuleFor(proxy => proxy.Nom).NotNull().NotEmpty()
                .WithMessage("The name should not be null or empty");
            RuleFor(proxy => proxy.Description).NotNull().NotEmpty()
                .WithMessage("The description should not be null or empty");
        }
    }
}
