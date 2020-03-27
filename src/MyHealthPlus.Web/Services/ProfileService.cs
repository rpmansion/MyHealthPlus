using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MyHealthPlus.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthPlus.Web.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<Account> _claimsFactory;
        private readonly UserManager<Account> _accountManager;

        public ProfileService(
            UserManager<Account> accountManager,
            IUserClaimsPrincipalFactory<Account> claimsFactory)
        {
            _accountManager = accountManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _accountManager.FindByIdAsync(sub);

            var principal = await _claimsFactory.CreateAsync(user);
            var userClaims = await _accountManager.GetClaimsAsync(user);

            var claims = principal.Claims
                // No need to include the AspNet.Identity claims (e.g. security stamp)
                .Where(x => !x.Type.Contains("AspNet.Identity"))
                .ToList();

            context.IssuedClaims = claims.Union(userClaims).ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var account = await _accountManager.FindByIdAsync(sub);
            context.IsActive = account != null;
        }
    }
}