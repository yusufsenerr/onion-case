using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.UserBalance.AddBalance
{
    public class AddUserBalanceCommandHandler<TDbContext>(
            IReadRepository<TDbContext, API.Common.Domain.Balance.UserBalance> readRepository,
            IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> writeRepository,
           IMapper mapper) : IRequestHandler<AddUserBalanceCommandRequest, BaseResponse> where TDbContext : DbContext
    {
        readonly private IMapper mapper = mapper;
        private readonly IReadRepository<TDbContext, API.Common.Domain.Balance.UserBalance> readRepository = readRepository;
        private readonly IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> writeRepository = writeRepository;
        public async Task<BaseResponse> Handle(AddUserBalanceCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var current = this.readRepository.GetWhere(x => x.AppUserId == request.AppUserId).FirstOrDefault();
                current.Amount = current.Amount + request.Balance;
                var success = this.writeRepository.Update(current);
                this.writeRepository.SaveAsync();
                return new BaseResponse { Message = "Bakiye başarılı şekilde eklendi", Succeeded = true };
            }
            catch (Exception ex)
            {

                WatchLogger.LogError($"Kayıt işleme sırasında bir istisna oluştu: {request.AppUserId}, Hata: {ex.Message}");
                return new BaseResponse
                {
                    Message = "Bir hata meydana geldi: " + ex.Message,
                    Succeeded = false,
                };
            }

        }
    }
}
