using System.Collections.Generic;
using System.Linq;

namespace UnrulableWallet.UI.Shared
{
    public static class Utils
    {
        /// <summary>
        /// Method to add an item to the list if value doesn't exist yet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns>Return list of values</returns>
        public static IEnumerable<T> AddIfNotExists<T>(this IEnumerable<T> list, T value)
        {
            if (!list.Contains(value))
            {
                return list.Append(value);
            }
            return list;
        }
    }
}
