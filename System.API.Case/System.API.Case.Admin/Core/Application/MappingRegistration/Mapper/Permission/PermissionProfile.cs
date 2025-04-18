using API.Common.Application.DTOs.Queries.Permission;
using AutoMapper;

namespace Application.MappingRegistration.Mapper.Permission
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<API.Common.Domain.Permissions.Permission, PermissionUserDto>();
        }
    }
}
