using System;
using System.Collections.Generic;

namespace Missile.Core
{
    public class TypeContainer : ITypeContainer
    {
        public HashSet<Type> Types { get; set; }

        internal TypeContainer()
        {
            Types = new HashSet<Type>();
        }

        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }
    }
}