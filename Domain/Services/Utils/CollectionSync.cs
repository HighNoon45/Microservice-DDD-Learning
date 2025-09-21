using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Utils
{
    public static class CollectionSync
    {
        /// <summary>
        /// Synchronizes an existing collection with a new collection.
        /// Executes remove, add and update actions accordingly.
        /// </summary>
        public static void SyncCollections<T>(
            IEnumerable<T> existing,
            IEnumerable<T> incoming,
            Func<T, int> getKey,              // how to identify an element (usually Id)
            Action<T> removeAction,           // what to do when element is missing
            Action<T> addAction,              // what to do when element is new
            Action<T, T> updateAction         // what to do when element exists in both
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
                if (!existingDict.TryGetValue(getKey(inc), out var ex))
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
