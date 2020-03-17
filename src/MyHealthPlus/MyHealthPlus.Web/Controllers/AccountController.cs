using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Data.Models;
using MyHealthPlus.Web.Models;
using System.Threading.Tasks;

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
        public IActionResult Login([FromBody]LoginModel model)
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var result = await _userManager.CreateAsync(userIdentity, model.Password);
            // var result = await _userManager.CreateAsync(new Account(), model)

            return Ok();
        }
    }
}