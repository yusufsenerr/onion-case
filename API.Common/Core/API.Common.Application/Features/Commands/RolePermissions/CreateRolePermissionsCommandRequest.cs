using API.Common.Application.DTOs.Commands.Autentication;
using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Role.Create;

public class CreateRolePermissionsCommandRequest : IRequest<BaseResponse>
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public List<MenuPermission> Permissions { get; set; }
}