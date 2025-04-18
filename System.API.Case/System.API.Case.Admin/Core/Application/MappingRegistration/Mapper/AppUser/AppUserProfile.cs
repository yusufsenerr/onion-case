using API.Common.Application.DTOs.Queries.Autentication;
using AutoMapper;

namespace Application.MappingRegistration.Mapper.AppUser
{
    public class AppUserProfile : Profile
    {
       public AppUserProfile()
       {
            CreateMap<API.Common.Domain.Users.AppUser, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IdentityNumber));

       }
    }
}
