using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Permissions.Create
{
    public class CreatePermissionsCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Pdf { get; set; }
        public bool Excel { get; set; }

    }
}
