using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Repositories.Authentication
{
    public interface IUserQueryRepo
    {
        User? GetUserByEmail(string email);
    }
}
