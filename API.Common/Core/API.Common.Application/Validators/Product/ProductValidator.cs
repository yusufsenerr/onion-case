using API.Common.Application.Features.Commands.Product.Create;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace API.Common.Application.Validators.Product
{
    public class ProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok 0'dan küçük olamaz.");

            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("En az bir resim yüklemelisiniz.");

            RuleForEach(x => x.Images)
                .SetValidator(new FormFileValidator());
        }
    }
    public class FormFileValidator : AbstractValidator<IFormFile>
        {
            public FormFileValidator()
            {
                RuleFor(file => file.Length)
                    .GreaterThan(0).WithMessage("Dosya boş olamaz.");

                RuleFor(file => file.ContentType)
                    .NotEmpty().WithMessage("Dosya tipi belirlenemedi.");
            }
        }
    }

