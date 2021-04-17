namespace Confab.Shared.Infrastructure
{
    using System.Collections.Generic;

    public static class CommonExtensions
    {
        public static ICollection<T> RemoveMany<T>(this ICollection<T> collection, IEnumerable<T> elementsToRemove)
        {
            foreach (var element in elementsToRemove)
            {
                collection.Remove(element);
            }

            return collection;
        }
    }
}