namespace BuberDinner.Application.Common.Interfaces.Authentication
{
    public interface IJwyTokenGenerator
    {
        string GenerateToken(Guid userId, string firstName, string lastName);
    }
}
