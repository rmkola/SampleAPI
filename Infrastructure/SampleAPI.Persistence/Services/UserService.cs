using Microsoft.AspNetCore.Identity;
using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.DTOs.User;
using SampleAPI.Application.Exceptions;
using SampleAPI.Application.Helpers;
using SampleAPI.Domain.Entities.Identity;

namespace SampleAPI.Persistence.Services
{
    /// <summary>
    /// Kullanıcı servisidir. 
    /// </summary>
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Verilen modeldeki değere sahip kullanıcı oluşturur.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
            }, model.Password);

            UserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        /// <summary>
        /// Verilen modeldeki id ye sahip olan kullanıcıyı siler.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserResponse> DeleteAsync(DeleteUser model)
        {
            IdentityResult result = await _userManager.DeleteAsync(new()
            {
                Id = model.Id.ToString(),
            });

            UserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla silinmiştir.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        /// <summary>
        /// Verilen modeldeki kullanıcıyı günceller.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserResponse> UpdateAsync(UpdateUser model)
        {
            IdentityResult result = await _userManager.UpdateAsync(new AppUser()
            { 
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
            });

            UserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla güncellenmiştir.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        /// <summary>
        /// Verilen refreshtoken bilgisi ile kullanıcı oturumunu yeniler.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="user"></param>
        /// <param name="accessTokenDate"></param>
        /// <param name="addOnAccessTokenDate"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundUserException"></exception>
        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        /// <summary>
        /// Kullanıcı parolasını günceller.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resetToken"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="PasswordChangeFailedException"></exception>
        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

       
    }
}
