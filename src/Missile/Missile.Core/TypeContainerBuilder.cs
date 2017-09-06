using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Missile.Core
{
    public class TypeContainerBuilder
    {
        protected TypeContainer typeContainer;

        public TypeContainerBuilder()
        {
            typeContainer = new TypeContainer();
        }

        public TypeContainerBuilder AddAssemblyTypes(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToList();
            types.ForEach(t => typeContainer.Types.Add(t));
            return this;
        }

        public TypeContainerBuilder AddTypes(params Type[] types)
        {
            types.ToList().ForEach(t => typeContainer.Types.Add(t));
            return this;
        }

        public TypeContainerBuilder AddTypes(IEnumerable<Type> types)
        {
            types.ToList().ForEach(t => typeContainer.Types.Add(t));
            return this;
        }

        public TypeContainer Build()
        {
            return typeContainer;
        }
    }
}