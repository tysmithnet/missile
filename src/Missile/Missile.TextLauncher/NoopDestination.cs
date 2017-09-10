using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestination))]
    public class NoOpDestination : IDestination<object>, IDestination
    {
        public string Name { get; set; } = "noop";

        public Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();

            source.Subscribe(o => tcs.SetResult(0), exception => tcs.SetException(exception),
                () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}