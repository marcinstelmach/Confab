namespace Confab.Modules.Identity.Core
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    internal interface IUsersRepository : IRepository
    {
        Task<User> GetAsync(Guid id);

        Task<User> GetAsync(string email);

        Task<bool> ExistsAsync(string email);

        void Add(User user);
    }
}