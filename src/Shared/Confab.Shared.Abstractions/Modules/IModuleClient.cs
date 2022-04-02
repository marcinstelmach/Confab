using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModuleClient
    {
        Task PublishAsync(object message);

        Task PublishAsync(DbContext context);
    }
}