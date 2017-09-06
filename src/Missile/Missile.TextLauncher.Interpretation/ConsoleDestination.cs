using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class ConsoleDestination : Destination<object>
    {
        public override Task ProcessAsync(IObservable<object> source)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            source.Subscribe(Console.WriteLine, exception => tcs.SetException(exception));

            return tcs.Task;
        }
    }
}
