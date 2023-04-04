using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UsersController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] CreateUserRequest request)
        {
            var user = _userService.CreateUser(request.Name, request.Email);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }

    }
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
