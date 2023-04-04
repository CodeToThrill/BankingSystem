using BankingSystem.Interfaces;
using BankingSystem.Models;

namespace BankingSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private int _nextTransactionId = 1;
        public void CreateTransaction(int accountId, decimal amount, string type)
        {
            var transaction = new Transaction { Id = _nextTransactionId++, AccountId = accountId, Amount = amount, Type = type };
            _transactions.Add(transaction);
        }

        public IEnumerable<Transaction> GetAccountTransactions(int accountId)
        {
            return _transactions.Where(t => t.AccountId == accountId);
        }
    }
}
