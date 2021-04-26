namespace Confab.Shared.Infrastructure.Contexts
{
    using Confab.Shared.Abstractions.Contexts;

    public interface IContextFactory
    {
        IContext Create();
    }
}