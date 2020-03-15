﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHealthPlus.Core;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthPlus.Data.Identity
{
    public class AccountStore : Disposable, IUserStore<Account>,
        IUserPasswordStore<Account>, IUserSecurityStampStore<Account>, IUserRoleStore<Account>
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

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
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

            var accountRole = account.AccountRoles.SingleOrDefault(x => x.Role.Name.ToUpperInvariant() == normalizedRole);

            if (accountRole != null)
            {
                throw new InvalidOperationException($"Account is already a {roleName}");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Name.ToUpperInvariant() == normalizedRole);

            if (role == null)
            {
                throw new InvalidOperationException($"Role {roleName} not found.");
            }

            account.AccountRoles.Add(new AccountRole { Account = account, Role = role });

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

            return Task.FromResult(account.AccountRoles.Any(x => x.Role.Name.ToUpperInvariant() == roleName.ToUpperInvariant()));
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
                x.Name.ToUpperInvariant() == roleName.ToUpperInvariant()
            );

            AccountRole accountRole = null;

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
    }
}