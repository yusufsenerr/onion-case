using API.Common.Application.DTOs.Queries.Permission;
using AutoMapper;

namespace Application.MappingRegistration.Mapper.MenuItem
{
    public class MenuPermissionProfile : Profile
    {
        public MenuPermissionProfile()
        {
            CreateMap<API.Common.Domain.Menu.MenuItem, MenuUserPermissionDto>();
        }
    }
}
