using System;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Tagging interface for filters
    /// </summary>
    public interface IFilter
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Represents a named object that is capable of transforming one observable into another
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDest">The type of the dest.</typeparam>
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
        /// <param name="args">The arguments.</param>
        /// <param name="source">The source.</param>
        /// <returns>
        ///     The filtered source
        /// </returns>
        IObservable<TDest> Filter(string[] args, IObservable<TSource> source);
    }
}