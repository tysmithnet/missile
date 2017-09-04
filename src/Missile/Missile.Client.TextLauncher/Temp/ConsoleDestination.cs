using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher.Temp
{
    public class ConsoleDestination : IDestination<string>
    {
        public Task Process(IObservable<string> source)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            source.Subscribe(Console.WriteLine, exception => Console.Error.WriteLine(exception),
                () => tcs.SetResult(null));
            return tcs.Task;
        }


        public string Name { get; } = "console";
    }
}
