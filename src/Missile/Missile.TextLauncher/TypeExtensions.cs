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
            if(type == null)
                yield break;

            if (type.IsInterface)
            {
                foreach (var iface in type.GetInterfaces())
                    yield return iface;
                yield break;
            }
            
            type = type.BaseType;    
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}