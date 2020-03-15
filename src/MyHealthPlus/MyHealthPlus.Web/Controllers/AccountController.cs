using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Web.Dtos;

namespace MyHealthPlus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            return Ok();
        }
    }
}