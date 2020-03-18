using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Data.Models;
using MyHealthPlus.Web.Models;
using System.Threading.Tasks;
using IdentityModel;

namespace MyHealthPlus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = new Account
            {
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(account, model.Password);

            if (!result.Succeeded)
            {
                // TODO : Log error
                return BadRequest(result.Errors.First().Description);
            }

            result = await _userManager.AddClaimsAsync(account, new Claim[]{
                new Claim(JwtClaimTypes.Name, $"{model.FirstName} {model.LastName}"),
                new Claim(JwtClaimTypes.MiddleName, model.MiddleName),
                new Claim(JwtClaimTypes.GivenName, model.FirstName),
                new Claim(JwtClaimTypes.FamilyName, model.LastName),
                new Claim(JwtClaimTypes.Email, model.Email),
                new Claim(JwtClaimTypes.Role, "Patient"),
                new Claim(JwtClaimTypes.PhoneNumber, "")
            });

            if (!result.Succeeded)
            {
                // TODO : Log error
                return BadRequest(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(account, "Patient");

            if (!result.Succeeded)
            {
                // TODO : Log error
                return BadRequest(result.Errors.First().Description);
            }

            return Ok("Successfully created an account for you.");
        }
    }
}