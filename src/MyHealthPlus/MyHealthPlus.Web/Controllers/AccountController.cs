using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Data.Models;
using MyHealthPlus.Web.Models;

namespace MyHealthPlus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<Account> userManager,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            return Ok();
        }
    }
}