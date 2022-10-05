namespace SampleAPI.Application.DTOs.User
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
    }
}
