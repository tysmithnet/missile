using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Determines the type used in
    /// </summary>
    /// <seealso cref="Missile.TextLauncher.IObservableInspector" />
    [Export(typeof(IObservableInspector))]
    public class SingleGenericTypeParameterInspector : IObservableInspector
    {
        public bool CanHandle(Type type)
        {
            return type.GenericTypeArguments.Length == 1 && type.GetInterfaces()
                       .Select(x => x.GetGenericTypeDefinition()).Contains(typeof(IObservable<>));
        }

        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }
}