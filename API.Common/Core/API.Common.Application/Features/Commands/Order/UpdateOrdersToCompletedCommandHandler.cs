using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Interfaces.IWriteRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Order
{
    public class UpdateOrdersToCompletedCommandHandler<TDbContext> : IRequestHandler<UpdateOrdersToCompletedCommand>
     where TDbContext : DbContext
    {
        private readonly IWriteRepository<TDbContext, API.Common.Domain.Orders.Order> _writeRepository;
        private readonly IReadRepository<TDbContext, API.Common.Domain.Orders.Order> _readRepository;

        public UpdateOrdersToCompletedCommandHandler(
            IWriteRepository<TDbContext, API.Common.Domain.Orders.Order> writeRepository,
            IReadRepository<TDbContext, API.Common.Domain.Orders.Order> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Unit> Handle(UpdateOrdersToCompletedCommand request, CancellationToken cancellationToken)
        {
            var orders = await _readRepository.GetWhere(o => o.Status != "Tamamlandı")
                                              .ToListAsync(cancellationToken);

            foreach (var order in orders)
            {
                order.Status = "Tamamlandı";
                await _writeRepository.CreateOrUpdateAsync(order);
            }

            return Unit.Value;
        }
    }

}
