using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.ProductImage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Product.Create
{
    public class CreateProductCommandHandler<TDbContext>(
           IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository
       ) : IRequestHandler<CreateProductCommandRequest, BaseResponse> where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository = writeRepository;
        public async Task<BaseResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var productImages = new List<ProductImage>();

                if (request.Images != null && request.Images.Any())
                {
                    foreach (var formFile in request.Images)
                    {
                        if (formFile.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await formFile.CopyToAsync(ms);
                                var fileBytes = ms.ToArray();
                                string base64 = Convert.ToBase64String(fileBytes);

                                productImages.Add(new ProductImage
                                {
                                    Id = Guid.NewGuid(),
                                    ImageBase64 = base64,
                                    CreatedDate = DateTime.UtcNow
                                });
                            }
                        }
                    }
                }

                var entity = new API.Common.Domain.Product.Product()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    CreatedDate = DateTime.UtcNow,
                    ProductImages = productImages
                };

                await this.writeRepository.CreateOrUpdateAsync(entity);

                return new()
                {
                    Succeeded = true,
                    Message = "Ürün başarılı şekilde oluşturuldu"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
        }

    }
}
