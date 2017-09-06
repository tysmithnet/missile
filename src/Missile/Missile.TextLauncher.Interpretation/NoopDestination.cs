using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class NoopDestination : Destination<object>
    {
        public override Task ProcessAsync(IObservable<object> source)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            source.Subscribe(o => tcs.SetResult(0), exception => tcs.SetException(exception),
                () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}