using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHealthPlus.Core;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthPlus.Data.Identity
{
    public class RoleStore : Disposable, IRoleStore<Role>
    {
        private readonly AppDbContext _context;

        public RoleStore(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (int.TryParse(roleId, out var id))
            {
                return await _context.Roles.FindAsync(id);
            }

            return await Task.FromResult((Role)null);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Roles.SingleOrDefaultAsync(x =>
                x.NormalizedName == normalizedRoleName,
                cancellationToken
            );
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Add(role);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Update(role);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Remove(role);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }
    }
}