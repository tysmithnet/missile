using System;

namespace Missile.TextLauncher.Provision
{
    /// <summary>
    ///     Represents an object that is capable of producing an observable sequence of values
    /// </summary>
    public interface IProvider
    {
    }

    /// <inheritdoc cref="IProvider" />
    public interface IProvider<out TDest> : IProvider
    {
        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        /// <value>
        ///     The name for this provider
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets the observable sequence of values this provider provides
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>The observable sequence of values this provider provides</returns>
        IObservable<TDest> Provide(string[] args);
    }
}