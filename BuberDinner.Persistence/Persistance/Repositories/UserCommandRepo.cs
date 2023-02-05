using BuberDinner.Application.Common.Interfaces.Repositories.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Persistence.Persistance.Repositories
{
    public class UserCommandRepo : IUserCommandRepo
    {
        public void AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            UserQueryRepo._users.Add(user);
        }
    }
}
