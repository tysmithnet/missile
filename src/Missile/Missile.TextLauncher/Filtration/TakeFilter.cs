using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a filter that will only provide up to a maximum number of values
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Filtration.IFilter{System.Object, System.Object}" />
    [Export(typeof(IFilter<object, object>))]
    public class TakeFilter : IFilter<object, object>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public string Name { get; set; } = "take";

        /// <summary>
        ///     Filters the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///     The filtered source
        /// </returns>
        /// <inheritdoc />
        public IObservable<object> Filter(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}