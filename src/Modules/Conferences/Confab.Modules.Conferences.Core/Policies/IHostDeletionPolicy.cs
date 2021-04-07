namespace Confab.Modules.Conferences.Core.Policies
{
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    public interface IHostDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Host host);
    }
}