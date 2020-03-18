using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHealthPlus.Core;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthPlus.Data.Identity
{
    public class AccountStore : Disposable, IUserStore<Account>,
        IUserClaimStore<Account>, IUserPasswordStore<Account>,
        IUserSecurityStampStore<Account>, IUserRoleStore<Account>
    {
        private readonly AppDbContext _context;

        public AccountStore(AppDbContext context)
        {
            _context = context;
        }

        #region IUserStore

        public async Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (int.TryParse(userId, out var id))
            {
                return await _context.Accounts.FindAsync(id);
            }

            return await Task.FromResult((Account)null);
        }

        public async Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Accounts.SingleOrDefaultAsync(
                x => x.UserName.ToUpper() == normalizedUserName, cancellationToken) as Account;
        }

        public Task<string> GetUserIdAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(account.Id.ToString());
        }

        public Task<string> GetNormalizedUserNameAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(account.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(Account account, string normalizedName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            account.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task<string> GetUserNameAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.UserName);
        }

        public Task SetUserNameAsync(Account account, string userName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            account.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Add(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> UpdateAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Update(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> DeleteAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Remove(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        #endregion IUserStore

        #region IUserPasswordStore

        public Task<bool> HasPasswordAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(!string.IsNullOrEmpty(account.PasswordHash));
        }

        public Task<string> GetPasswordHashAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.PasswordHash);
        }

        public Task SetPasswordHashAsync(Account account, string passwordHash, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            account.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        #endregion IUserPasswordStore

        #region IUserSecurityStampStore

        public Task<string> GetSecurityStampAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.SecurityStamp);
        }

        public Task SetSecurityStampAsync(Account account, string stamp, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(stamp))
            {
                throw new ArgumentNullException(nameof(stamp));
            }

            account.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        #endregion IUserSecurityStampStore

        #region IUserRoleStore

        public async Task AddToRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var normalizedRole = roleName.ToUpperInvariant();

            var accountRole = account.AccountRoles.SingleOrDefault(x => x.Role.NormalizedName == normalizedRole);

            if (accountRole != null)
            {
                throw new InvalidOperationException($"Account is already a {roleName}");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(x => x.NormalizedName == normalizedRole);

            if (role == null)
            {
                throw new InvalidOperationException($"Role {roleName} not found.");
            }

            account.AccountRoles.Add(new Account2Role { Account = account, Role = role });

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var query = (from accountRole in _context.AccountRoles
                         where accountRole.Account == account
                         join role in _context.Roles on accountRole.Role equals role
                         select role.Name);

            return await query.ToListAsync();
        }

        public async Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var accounts = _context.AccountRoles
                .Where(x => x.Role.Name == roleName)
                .Select(x => x.Account);

            return await accounts.ToListAsync(cancellationToken);
        }

        public Task<bool> IsInRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return Task.FromResult(account.AccountRoles.Any(x => x.Role.NormalizedName == roleName.ToUpperInvariant()));
        }

        public async Task RemoveFromRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidOperationException($"Role {roleName} not found.");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(x =>
                x.NormalizedName == roleName.ToUpperInvariant()
            );

            Account2Role accountRole = null;

            if (role != null)
            {
                accountRole = await _context.AccountRoles.FirstOrDefaultAsync(x =>
                    x.Role == role && x.Account == account
                );
            }

            if (accountRole != null)
            {
                account.AccountRoles.Remove(accountRole);
                _context.AccountRoles.Remove(accountRole);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion IUserRoleStore

        #region IUserClaimStore

        public async Task<IList<Claim>> GetClaimsAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return await _context.AccountClaims
                .Where(x => x.Account.Id.Equals(account.Id))
                .Select(y => y.ToClaim())
                .ToListAsync(cancellationToken);
        }

        public async Task AddClaimsAsync(Account account, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            foreach (var claim in claims)
            {
                _context.AccountClaims.Add(new AccountClaim
                {
                    Account = account,
                    Type = claim.Type,
                    Value = claim.Value
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ReplaceClaimAsync(Account account, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }

            var matches = await _context.AccountClaims
                .Where(x => x.Account == account
                            && x.Type == claim.Type && x.Value == claim.Value)
                .ToListAsync(cancellationToken);

            foreach (var match in matches)
            {
                match.Type = newClaim.Type;
                match.Value = newClaim.Value;
            }
        }

        public async Task RemoveClaimsAsync(Account account, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            var matches = new List<AccountClaim>();

            foreach (var claim in claims)
            {
                matches = await _context.AccountClaims
                    .Where(x => x.Account == account && x.Type == claim.Type && x.Value == claim.Value)
                    .ToListAsync(cancellationToken);
            }

            foreach (var claim in matches)
            {
                _context.AccountClaims.Remove(claim);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<IList<Account>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            // NOTE : not need for this time

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        #endregion IUserClaimStore
    }
}