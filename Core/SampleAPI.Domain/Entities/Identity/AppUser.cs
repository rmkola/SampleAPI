using Microsoft.AspNetCore.Identity;

namespace SampleAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {

        public string? NameSurname { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<Basket>? Baskets { get; set; }
    }
}
