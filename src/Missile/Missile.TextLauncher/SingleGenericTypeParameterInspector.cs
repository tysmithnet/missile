using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Observable inspector that is able to determine the generic type of an observable if
    ///     it has a single generic type parameter
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IObservableInspector" />
    [Export(typeof(IObservableInspector))]
    public class SingleGenericTypeParameterInspector : IObservableInspector
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determines whether this instance can handle the specified type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///     <c>true</c> if this instance can handle the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanHandle(Type type)
        {
            return type.GenericTypeArguments.Length == 1 && type.GetInterfaces()
                       .Select(x => x.GetGenericTypeDefinition()).Contains(typeof(IObservable<>));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the type of the observable
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///     The type of the observable
        /// </returns>
        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }
}