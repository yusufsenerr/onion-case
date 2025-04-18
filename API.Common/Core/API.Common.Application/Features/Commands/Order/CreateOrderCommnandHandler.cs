using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Orders;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Order
{
    public class CreateOrderCommandHandler<TDbContext>(
    IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository,
    IReadRepository<TDbContext, API.Common.Domain.Product.Product> readRepository,
    IWriteRepository<TDbContext, API.Common.Domain.Orders.Order> orderWriteRepository,
    IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> writeBalanceWriteRepository,
    IReadRepository<TDbContext, API.Common.Domain.Balance.UserBalance> readBalanceWriteRepository,
           UserManager<AppUser> userManager
       ) : IRequestHandler<CreateOrderCommandRequest, BaseResponse> where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository = writeRepository;
        readonly private IReadRepository<TDbContext, API.Common.Domain.Product.Product> readRepository = readRepository;
        private readonly IWriteRepository<TDbContext, API.Common.Domain.Orders.Order> _orderRepository = orderWriteRepository;
        private readonly IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> writeBalanceWriteRepository = writeBalanceWriteRepository;
        private readonly IReadRepository<TDbContext, API.Common.Domain.Balance.UserBalance> readBalanceWriteRepository = readBalanceWriteRepository;
        readonly UserManager<AppUser> userManager = userManager;
        public async Task<BaseResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.AppUserId.ToString());
            if (user == null)
            {
                return new BaseResponse { Succeeded = false, Message = "Kullanıcı bulunamadı." };
            }

            var balance = await readBalanceWriteRepository.GetWhere(x => x.AppUserId == request.AppUserId).FirstOrDefaultAsync();
            if (balance == null)
            {
                return new BaseResponse { Succeeded = false, Message = "Kullanıcı bakiyesi bulunamadı." };
            }

            decimal totalPrice = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in request.Items)
            {
                var product = await this.readRepository.GetWhere(p => p.Id == item.ProductId).FirstOrDefaultAsync();
                if (product == null)
                    return new BaseResponse { Succeeded = false, Message = "Ürün bulunamadı." };

                if (product.Stock < item.Quantity)
                    return new BaseResponse { Succeeded = false, Message = $"'{product.Name}' ürünü için stok yetersiz." };

                totalPrice += product.Price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            if (balance.Amount < totalPrice)
            {
                return new BaseResponse
                {
                    Succeeded = false,
                    Message = "Bakiye yetersiz."
                };
            }

            // Sipariş oluştur
            var order = new API.Common.Domain.Orders.Order
            {
                Id = Guid.NewGuid(),
                AppUserId = request.AppUserId,
                CreatedDate = DateTime.Now,
                TotalAmount = totalPrice,
                OrderItems = orderItems,
                Status = "Hazırlanıyor"
            };

            await _orderRepository.CreateOrUpdateAsync(order);

            // Stok düş
            foreach (var item in request.Items)
            {
                var product = await readRepository.GetWhere(p => p.Id == item.ProductId).FirstOrDefaultAsync();
                product.Stock -= item.Quantity;
                await writeRepository.CreateOrUpdateAsync(product);
            }

            // Bakiye düş
            balance.Amount -= totalPrice;
            await writeBalanceWriteRepository.CreateOrUpdateAsync(balance);

            return new BaseResponse
            {
                Succeeded = true,
                Message = "Sipariş başarıyla oluşturuldu."
            };

        }
    }
}
