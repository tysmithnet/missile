using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Represents a destination that doesn't do anything except wait for the source to finish
    /// </summary>
    [Export(typeof(IDestination))]
    public class NoOpDestination : IDestination<object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "noop";

        /// <inheritdoc />
        public async Task ProcessAsync(IObservable<object> source)
        {
            var result = await source.LastOrDefaultAsync();
        }
    }
}