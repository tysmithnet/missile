using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a destination that doesn't do anything except wait for the source to finish
    /// </summary>
    [Export(typeof(IDestination))]
    public class NoOpDestination : IDestination<object>
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
        ///     Processes the input items asynchronously
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task that when complete will signal the completion of the processing
        /// </returns>
        /// <inheritdoc />
        public async Task ProcessAsync(IObservable<object> source, CancellationToken cancellationToken)
        {
            var result = await source.LastOrDefaultAsync();
        }
    }
}