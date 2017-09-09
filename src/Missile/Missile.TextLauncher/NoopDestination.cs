using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(Destination<object>))]
    public class NoOpDestination : Destination<object>
    {
        public override string Name { get; set; } = "noop";

        public override Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();

            source.Subscribe(o => tcs.SetResult(0), exception => tcs.SetException(exception),
                () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}