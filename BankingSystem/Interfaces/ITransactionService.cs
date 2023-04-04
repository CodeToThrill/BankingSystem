using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(int accountId, decimal amount, string type);
        IEnumerable<Transaction> GetAccountTransactions(int accountId);
    }
}
