using System;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Tagging interface for filters
    /// </summary>
    public interface IFilter
    {
    }

    /// <summary>
    ///     Represents an object is capable of transforming an observable
    /// </summary>
    /// <typeparam name="TSource">Source type of the observalbe e.g. IObservable<string></typeparam>
    /// <typeparam name="TDest">Destination type of the observalbe e.g. IObservable<string></typeparam>
    public interface IFilter<in TSource, out TDest> : IFilter
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Filters the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The filtered source</returns>
        IObservable<TDest> Filter(IObservable<TSource> source);
    }
}