using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.SystemAdmin
{
    public class CreateSystemAdminCommandRequest : IRequest<BaseResponse>
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
    }
}
