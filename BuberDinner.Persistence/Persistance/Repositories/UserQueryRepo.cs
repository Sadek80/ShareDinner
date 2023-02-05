using BuberDinner.Application.Common.Interfaces.Repositories.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Persistence.Persistance.Repositories
{
    public class UserQueryRepo : IUserQueryRepo
    {
        public static List<User> _users = new List<User>();

        public User? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }
    }
}
