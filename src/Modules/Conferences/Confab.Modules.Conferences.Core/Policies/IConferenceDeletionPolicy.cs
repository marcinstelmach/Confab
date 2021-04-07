namespace Confab.Modules.Conferences.Core.Policies
{
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    public interface IConferenceDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Conference conference);
    }
}