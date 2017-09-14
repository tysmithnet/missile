using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestination))]
    public class NoOpDestination : IDestination<object>
    {
        public string Name { get; set; } = "noop";

        public async Task ProcessAsync(IObservable<object> source)
        {
            var result = await source.LastOrDefaultAsync();
        }
    }
}