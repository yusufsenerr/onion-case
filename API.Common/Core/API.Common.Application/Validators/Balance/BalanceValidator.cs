using API.Common.Application.Features.Commands.UserBalance.AddBalance;
using FluentValidation;

namespace API.Common.Application.Validators.Balance
{
    public class BalanceValidator : AbstractValidator<AddUserBalanceCommandRequest>
    {
        public BalanceValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.Balance)
                .GreaterThan(0).WithMessage("Bakiye 0'dan büyük olmalıdır.");
        }
    }
}
