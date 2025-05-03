using API.Common.Application.Features.Commands.Authentications.Create.Register;
using FluentValidation;

namespace API.Common.Application.Validators.User
{
    public class AppUserValidator : AbstractValidator<RegisterUserCommandRequest>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad 50 karakterden uzun olamaz.");

            RuleFor(x => x.IdentityType)
                .NotEmpty().WithMessage("Kimlik tipi boş olamaz.")
                .MaximumLength(20).WithMessage("Kimlik tipi 20 karakterden uzun olamaz.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Kimlik numarası boş olamaz.")
                .Matches(@"^\d{11}$").WithMessage("Kimlik numarası 11 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Rol Id boş olamaz.");
        }
    }
}

