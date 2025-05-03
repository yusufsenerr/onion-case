using API.Common.Application.DTOs.Commands.Order;
using FluentValidation;

namespace API.Common.Application.Validators.Order
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün ID boş olamaz.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Adet 0'dan büyük olmalıdır.");
        }
    }
}
