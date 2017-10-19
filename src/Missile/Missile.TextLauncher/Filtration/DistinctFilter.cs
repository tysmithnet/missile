using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Filter that does not allow duplicate values to pass through
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Filtration.IFilter{System.Object, System.Object}" />
    [Export(typeof(IFilter<object, object>))]
    public class DistinctFilter : IFilter<object, object>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "distinct";

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
            return source.Distinct();
        }
    }
}