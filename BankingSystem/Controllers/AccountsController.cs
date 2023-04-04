using BankingSystem.Interfaces;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountsController(ITransactionService transactionService, IAccountService accountService, IUserService userService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _userService = userService;
        }
        [HttpPost("{id}")]
        public ActionResult<Account> CreateAccount(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            var account = _accountService.CreateAccount(id);
            user.Accounts.Add(account);


            return Ok(account);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _accountService.DeleteAccount(id);
            foreach (User user in _userService.GetUsers())
            {
                user.Accounts = user.Accounts.Where(a => a.Id != id).ToList();
            }
            return NoContent();
        }
        [HttpPost("{id}/deposits")]
        public ActionResult<Transaction> Deposit(int id, [FromBody] DepositRequest request)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            var accounts = _accountService.GetUserAccounts(id);
            if (accounts == null)
            {
                return NotFound();
            }
            if (!accounts.Any(c=>c.Id.Equals(request.AccountID)))
            {
                return NotFound();
            }
            try
            {
                _accountService.Deposit(request.AccountID, request.Amount);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(_transactionService.GetAccountTransactions(request.AccountID));
        }

        [HttpPost("{id}/withdrawals")]
        public ActionResult<Transaction> Withdraw(int id, [FromBody] WithdrawRequest request)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            var accounts = _accountService.GetUserAccounts(id);
            if (accounts == null)
            {
                return NotFound();
            }
            if (!accounts.Any(c => c.Id.Equals(request.AccountID)))
            {
                return NotFound();
            }

            try
            {
                _accountService.Withdraw(request.AccountID, request.Amount);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(_transactionService.GetAccountTransactions(request.AccountID));
        }
    }
    public class DepositRequest
    {
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawRequest
    {
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
