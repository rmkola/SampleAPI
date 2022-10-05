using SampleAPI.Application.Abstractions.Services.Authentications;

namespace SampleAPI.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication
    {
        Task PasswordResetAsnyc(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
