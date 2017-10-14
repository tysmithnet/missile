using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Provision
{
    /// <summary>
    /// A provider that immediately completes the observable sequence
    /// </summary>
    /// <seealso cref="object" />
    /// <inheritdoc />
    /// <seealso cref="T:System.Object" />
    [Export(typeof(IProvider))]
    public class NoOpProvider : IProvider<object>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name for this provider
        /// </value>
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "noop";

        /// <summary>
        /// Gets the observable sequence of values this provider provides
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>
        /// The observable sequence of values this provider provides
        /// </returns>
        /// <inheritdoc />
        public IObservable<object> Provide(string[] args)
        {
            return new object[0].ToObservable();
        }
    }
}