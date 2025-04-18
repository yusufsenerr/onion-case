using API.Common.Domain.Roles;
using MediatR;

namespace API.Common.Application.Features.Queries.Role
{
    public class GetAllRoleQueryRequest : IRequest<List<AppRole>>
    {
        public Guid? FirmId { get; set; }
    }
}
