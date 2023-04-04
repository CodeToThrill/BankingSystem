using BankingSystem.Interfaces;
using BankingSystem.Models;

namespace BankingSystem.Services
{
    public class UserService:IUserService
    {
        private readonly List<User> _users = new List<User>();
        private int _nextUserId = 1;

        public User CreateUser(string name, string email)
        {
            var user = new User { Id = _nextUserId++, Name = name, Email = email };
            _users.Add(user);
            return user;
        }

        public void DeleteUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}
