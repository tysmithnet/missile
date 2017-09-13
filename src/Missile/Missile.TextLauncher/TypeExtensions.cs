using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Missile.TextLauncher
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Type itr = type;
            while (itr != null)
            {
                yield return itr;
                itr = itr.BaseType;
            }
        }
    }
}