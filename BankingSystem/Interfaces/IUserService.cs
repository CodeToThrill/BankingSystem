using BankingSystem.Models;

namespace BankingSystem.Interfaces
 
{
    public interface IUserService
    {
        User CreateUser(string name, string email);
        void DeleteUser(int userId);
        User GetUser(int userId);
        IEnumerable<User> GetUsers();
    }
}
