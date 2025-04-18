using API.Common.Application.DTOs.Commands.Autentication;
using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Authentications.Create.Register
{
    public class RegisterUserCommandRequest : IRequest<BaseResponse>
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public Guid RoleId { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
    }
}