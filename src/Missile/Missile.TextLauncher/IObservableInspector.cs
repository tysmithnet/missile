using System;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents an object that is capable of determining the type of an IObservable
    /// </summary>
    public interface IObservableInspector
    {
        /// <summary>
        ///     Determines whether this instance can handle the specified type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///     <c>true</c> if this instance can handle the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanHandle(Type type);

        /// <summary>
        ///     Gets the type of the observable
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The type of the observable</returns>
        Type GetObservableType(Type type);
    }
}