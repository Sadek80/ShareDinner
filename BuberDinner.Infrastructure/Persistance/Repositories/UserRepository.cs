using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }
    }
}
