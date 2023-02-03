namespace BuberDinner.Contracts.Authentication
{
    public record LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
