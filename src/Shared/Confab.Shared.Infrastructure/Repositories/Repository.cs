namespace Confab.Shared.Infrastructure.Repositories
{
    using Confab.Shared.Abstractions.Repositories;

    public class Repository : IRepository
    {
        public Repository(IUnitOfWork context)
        {
            UnitOfWork = context;
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}