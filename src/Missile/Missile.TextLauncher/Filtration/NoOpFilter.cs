using System;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Filter that does no transformation
    /// </summary>
    public class NoOpFilter : IFilter<object, object>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "noop";

        /// <summary>
        ///     Filters the specified source.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="source">The source.</param>
        /// <returns>
        ///     The filtered source
        /// </returns>
        /// <inheritdoc />
        public IObservable<object> Filter(string[] args, IObservable<object> source)
        {
            return source;
        }
    }
}