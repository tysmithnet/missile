using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Filter that returns the first value and then closes
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Filtration.IFilter{System.Object, System.Object}" />
    public class FirstFilter : IFilter<object, object>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public string Name { get; set; } = "first";

        /// <summary>
        ///     Allows only the first item to pass
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IObservable<object> Filter(IObservable<object> source)
        {
            return source.FirstAsync();
        }
    }
}