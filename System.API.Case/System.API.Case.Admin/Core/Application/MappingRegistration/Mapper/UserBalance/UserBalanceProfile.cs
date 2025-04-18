using API.Common.Application.DTOs.Queries.Balance;
using AutoMapper;

namespace Application.MappingRegistration.Mapper.UserBalance
{
    public class UserBalanceProfile : Profile
    {
        public UserBalanceProfile()
        {
            CreateMap<API.Common.Domain.Balance.UserBalance, UserBalanceDto>()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
