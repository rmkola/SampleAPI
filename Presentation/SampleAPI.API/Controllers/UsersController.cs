using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.Features.Commands.AppUser.CreateUser;
using SampleAPI.Application.Features.Commands.AppUser.UpdatePassword;
using SampleAPI.Application.Features.Commands.AppUser.UpdateUser;

namespace SampleAPI.API.Controllers
{
    /// <summary>
    /// Kullanıcılar için hazırlanan APIdir.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailService;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mailService"></param>
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        /// <summary>
        /// Kullanıcı kaydını yapan metoddur.
        /// </summary>
        /// <param name="createUserCommandRequest">createUserCommandRequest türünden parametre gerektirir.</param>
        /// <returns>CreateUserCommandResponse türünden veri döner.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Kullanıcı bilgilerini güncellemeyi sağlayan metoddur.
        /// </summary>
        /// <param name="updateUserCommandRequest">updateUserCommandRequest türünden veri gerektirir.</param>
        /// <returns>UpdateUserCommandResponse türünden veri döner. </returns>
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdateUserCommandResponse response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Kullanıcı silmeyi sağlayan metoddur.
        /// </summary>
        /// <param name="deleteUserCommandRequest">deleteUserCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UpdateUserCommandResponse türünden veri döner.</returns>
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(DeleteUserCommandRequest deleteUserCommandRequest)
        {
            UpdateUserCommandResponse response = await _mediator.Send(deleteUserCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Kullanıcı parolasını güncelleyen metoddur.
        /// </summary>
        /// <param name="updatePasswordCommandRequest">updatePasswordCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UpdatePasswordCommandResponse türünden veri döner.</returns>
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }
    }
}
