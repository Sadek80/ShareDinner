using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Repositories.Authentication
{
    public interface IUserCommandRepo
    {
        void AddUser(User user);
    }
}
