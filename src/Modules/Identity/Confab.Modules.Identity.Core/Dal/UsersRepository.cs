namespace Confab.Modules.Identity.Core.Dal
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Entities;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class UsersRepository : Repository, IUsersRepository
    {
        private readonly IdentityDbContext _context;

        public UsersRepository(IdentityDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }
}