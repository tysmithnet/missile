using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    [Export(typeof(IDestination))]
    public class ConsoleDestination : IDestination<object>
    {
        internal Action<object> WriteFunction = Console.WriteLine;

        public string Name { get; set; } = "console";

        public Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(WriteFunction, exception => tcs.SetException(exception), () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}