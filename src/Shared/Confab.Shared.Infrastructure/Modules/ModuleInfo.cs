namespace Confab.Shared.Infrastructure.Modules
{
    using System.Collections.Generic;

    internal record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}