using SampleAPI.Application.DTOs.User;
using SampleAPI.Application.Features.Commands.AppUser.CreateUser;
using SampleAPI.Domain.Entities.Identity;

namespace SampleAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserResponse> CreateAsync(CreateUser model);
        Task<UserResponse> UpdateAsync(UpdateUser model);
        Task<UserResponse> DeleteAsync(DeleteUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    }
}
