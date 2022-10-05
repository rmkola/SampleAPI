using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.Abstractions.Token;
using SampleAPI.Application.DTOs;
using SampleAPI.Application.Exceptions;
using SampleAPI.Application.Helpers;
using SampleAPI.Domain.Entities.Identity;

namespace SampleAPI.Persistence.Services
{
    /// <summary>
    /// Auth Servisidir. Login esnasında token üretir. 
    /// sonraki işlemlerde bu token kullanılır.
    /// Token süresi bitince tekrar token alınması gerekir.
    /// </summary>
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;
        public AuthService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService,
            IMailService mailService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }
 /// <summary>
 /// Login sırasında çalışan metoddur.
 /// </summary>
 /// <param name="usernameOrEmail">Kullanıcı adı veya mail adresi girilmelidir.</param>
 /// <param name="password">Kullanıcı parolası</param>
 /// <param name="accessTokenLifeTime">Dakika cinsinden token süresini belirtir.</param>
 /// <returns>Geriye token türünden veri döndürür.</returns>
 /// <exception cref="NotFoundUserException">Kullanıcı bulunamazsa bu hata fırlatılır.</exception>
 /// <exception cref="AuthenticationErrorException">Kullanıcı adı veya parola uyuşmazlığında bu hata fırlatılır.</exception>
        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new AuthenticationErrorException();
        }

        /// <summary>
        /// Token süresi geçen kullanıcılar için ekstra 15 dk. lık süre verir.
        /// </summary>
        /// <param name="refreshToken">Token üretimi sırasında belirtilen refresh token değeri parametre olarak verilmelidir.</param>
        /// <returns>Geriye token türünden veri döndürür.</returns>
        /// <exception cref="NotFoundUserException">Refresh token a konu olan kullanıcı bulunamazsa bu hata döner.</exception>
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

        /// <summary>
        /// Kullanıcı mailine parola resetlemesi için link gönderilir.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task PasswordResetAsnyc(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        /// <summary>
        /// Verilen token geçerli ise true döner. geçerli bir token değilse false döner.
        /// </summary>
        /// <param name="resetToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
