using System;
using System.Collections.Generic;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Extension methods for runtime types
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Get this type followed by all base types in an enumeration
        /// </summary>
        /// <param name="type">Type to get base types for</param>
        /// <returns>Get this type followed by all base types in an enumeration</returns>
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            var itr = type;
            while (itr != null)
            {
                yield return itr;
                itr = itr.BaseType;
            }
        }
    }
}