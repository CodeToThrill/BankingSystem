using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> _accounts = new List<Account>();
        private int _nextAccountId = 1;
        private ITransactionService _transactionService;
        private IUserService _userService;
        public AccountService(ITransactionService transactionService,IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        public Account CreateAccount(int userId)
        {
            var account = new Account { Id = _nextAccountId++, UserId = userId, Balance = 100 };
            _accounts.Add(account);
            return account;
        }

        public void DeleteAccount(int accountId)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                _accounts.Remove(account);
            }
        }

        public Account GetAccount(int accountId)
        {
            return _accounts.FirstOrDefault(a => a.Id == accountId);
        }

        public IEnumerable<Account> GetUserAccounts(int userId)
        {
            return _accounts.Where(a => a.UserId == userId);
        }

        public decimal GetAccountBalance(int accountId)
        {
            var account = GetAccount(accountId);
            return account != null ? account.Balance : 0;
        }

        public void Deposit(int accountId, decimal amount)
        {
            if (amount > 10000)
            {
                throw new InvalidOperationException("Deposit amount exceeds maximum limit of $10,000");
            }

            var account = GetAccount(accountId);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found");
            }

            account.Balance += amount;

            _transactionService.CreateTransaction(accountId, amount, "Deposit");
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = GetAccount(accountId);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found");
            }

            var maxWithdrawal = account.Balance * 0.9m;
            if (amount > maxWithdrawal)
            {
                throw new InvalidOperationException($"Withdrawal amount exceeds maximum limit of {maxWithdrawal:C}");
            }

            if (account.Balance - amount < 100)
            {
                throw new InvalidOperationException("Account balance cannot be less than $100");
            }
            account.Balance -= amount;
            _transactionService.CreateTransaction(accountId, amount, "Withdrawal");
        }
    }
}
