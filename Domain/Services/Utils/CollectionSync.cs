using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Utils
{
    public static class CollectionSync //Do not redeem
    {
        public static void SyncCollections<T>(
            IEnumerable<T> existing,
            IEnumerable<T> incoming,
            Func<T, Guid> getKey,
            Action<T> removeAction,
            Action<T> addAction,
            Action<T, T> updateAction
        ) where T : class, IPricingPrincipal
        {
            var existingDict = existing.ToDictionary(getKey);
            var incomingDict = incoming.ToDictionary(getKey);

            // Remove items that are no longer present
            foreach (var ex in existingDict.Values)
            {
                if (!incomingDict.ContainsKey(getKey(ex)))
                    removeAction(ex);
            }

            // Add or update items
            foreach (var inc in incomingDict.Values)
            {
                if (!existingDict.TryGetValue(getKey(inc), out T? ex))
                {
                    addAction(inc);
                }
                else
                {
                    updateAction(ex, inc);
                }
            }
        }
    }
}
