using System;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public class ConsoleDestination : Destination<object>
    {
        internal Action<object> WriteFunction = Console.WriteLine;

        public override Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(WriteFunction, exception => tcs.SetException(exception), () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}