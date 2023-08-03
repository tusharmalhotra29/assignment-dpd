namespace Project1.Models.DTO
{
    public class UserRegistrationRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
    }
}
