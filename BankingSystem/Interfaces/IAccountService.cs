using BankingSystem.Models;
using System.Security.Principal;

namespace BankingSystem.Interfaces
{
    public interface IAccountService
    {
        Account CreateAccount(int userId);
        void DeleteAccount(int accountId);
        Account GetAccount(int accountId);
        IEnumerable<Account> GetUserAccounts(int userId);
        decimal GetAccountBalance(int accountId);
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
    }
}
