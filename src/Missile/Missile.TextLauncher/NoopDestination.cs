using System;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public class NoopDestination : Destination<object>
    {
        public override Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();

            source.Subscribe(o => tcs.SetResult(0), exception => tcs.SetException(exception),
                () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}