using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    /// <inheritdoc cref="IObservableInspector" />
    [Export(typeof(IObservableInspector))]
    public class SingleGenericTypeParameterInspector : IObservableInspector
    {
        /// <inheritdoc />
        public bool CanHandle(Type type)
        {
            return type.GenericTypeArguments.Length == 1 && type.GetInterfaces()
                       .Select(x => x.GetGenericTypeDefinition()).Contains(typeof(IObservable<>));
        }

        /// <inheritdoc />
        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }
}