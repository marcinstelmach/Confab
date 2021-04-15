namespace Confab.Shared.Abstractions.Repositories
{
    public interface IRepository
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}