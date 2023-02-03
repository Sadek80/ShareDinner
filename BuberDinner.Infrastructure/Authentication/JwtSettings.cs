namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string _section = "JwtSettings";

        public string Secret { get; init; }
        public int ExpiryInMinutes { get; init; } 
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
