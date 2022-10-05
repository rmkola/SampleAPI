using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Features.Commands.AppUser.LoginUser;
using SampleAPI.Application.Features.Commands.AppUser.PasswordReset;
using SampleAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;
using SampleAPI.Application.Features.Commands.AppUser.VerifyResetToken;

namespace SampleAPI.API.Controllers
{

    /// <summary>
    /// Kullanıcı işlemlerini yapan APIdir..
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Giriş yapmak için kullanılan API metodudur.
        /// </summary>
        /// <param name="loginUserCommandRequest">Giriş yapan kullanıcının parametre bilgilerini içerir.</param>
        /// <returns>Geriye LoginUserCommandResponse türünden giriş bilgilerini döner.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Giriş yapan kullanıcı var ise Bu metodla tekrar giriş yapabilir.
        /// </summary>
        /// <param name="refreshTokenLoginCommandRequest">refreshTokenLoginCommandRequest türünde parametre gerektirir</param>
        /// <returns>Geriye RefreshTokenLoginCommandResponse türünden giriş bilgilerini döner.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }




        /// <summary>
        /// Kullanıcı parolasını sıfırlamak için gerekli metoddur.
        /// </summary>
        /// <param name="passwordResetCommandRequest">passwordResetCommandRequest türünden parametre gerektirir.</param>
        /// <returns>PasswordResetCommandResponse türünde veri döner.</returns>
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Kullanıcının mailinden dönen resettoken türünden talebin doğruluğunu kontrol eder.
        /// </summary>
        /// <param name="verifyResetTokenCommandRequest">verifyResetTokenCommandRequest türünden parametre gerektirir.</param>
        /// <returns>VerifyResetTokenCommandResponse türünden veri döner.</returns>
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }

    }
}
