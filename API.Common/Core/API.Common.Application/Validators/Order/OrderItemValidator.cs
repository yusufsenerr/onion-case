using FluentValidation;
using API.Common.Domain.Orders;
using API.Common.Application.Features.Commands.Order.Create; // Dto burada ise

namespace API.Common.Application.Validators.Order
{
    public class OrderItemValidator : AbstractValidator<CreateOrderCommandRequest>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sipariş kalemleri boş olamaz.")
                .Must(list => list != null && list.Count > 0)
                    .WithMessage("En az bir sipariş kalemi girmelisiniz.");

            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemDtoValidator());
        }
    }
}

