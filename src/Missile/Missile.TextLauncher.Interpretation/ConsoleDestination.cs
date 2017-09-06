using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class ConsoleDestination : Destination<object>
    {
        internal Action<object> WriteFunction = Console.WriteLine;

        public override Task ProcessAsync(IObservable<object> source)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            source.Subscribe(WriteFunction, exception => tcs.SetException(exception), () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}
