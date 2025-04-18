using API.Common.Application.Features.Commands.Authentications.Create.Login;
using API.Common.Application.Features.Commands.Authentications.Create.Register;
using API.Common.Application.Features.Commands.Authentications.Remove;
using API.Common.Application.Features.Commands.Authentications.Update;
using API.Common.Application.Features.Queries.Authentications;
using API.Common.Application.Services.LogService;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator,LoggingService loggingService) : ControllerBase
    {
        readonly IMediator mediator = mediator;
        private readonly LoggingService loggingService = loggingService;

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllUser(GetAllUserQueryRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserById(GetUserByIdQueryRequest getUserByIdQueryRequest)
        {
            var response = await this.mediator.Send(getUserByIdQueryRequest);
            return Ok(response);
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(RegisterUserCommandRequest createUserCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest createUserCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveUser(RemoveUserCommandRequest removeUserCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(removeUserCommandRequest);
            return Ok(response);
        }
        //[HttpPost("[action]")]
        //public async Task<IActionResult> PasswordChange(ChangePasswordCommandRequest request)
        //{
        //    BaseResponse response = await this.mediator.Send(request);
        //    return Ok(response);
        //}
    }
}
