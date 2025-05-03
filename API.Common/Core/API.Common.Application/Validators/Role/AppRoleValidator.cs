using API.Common.Application.Features.Commands.Role.Create;
using FluentValidation;

namespace API.Common.Application.Validators.Role
{
    public class AppRoleValidator : AbstractValidator<RegisterRoleCommandRequest>
    {
        public AppRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol ismi boş olamaz.")
                .MaximumLength(100).WithMessage("Rol ismi en fazla 100 karakter olabilir.");
        }
    }
}
