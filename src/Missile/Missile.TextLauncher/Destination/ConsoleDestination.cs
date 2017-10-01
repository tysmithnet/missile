using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Destination that will output source items to the Console
    /// </summary>
    [Export(typeof(IDestination))]
    public class ConsoleDestination : IDestination<object>
    {
        /// <summary>
        ///     Function to invoke when writing to Console
        /// </summary>
        protected internal virtual Action<object> WriteFunction { get; set; } = Console.WriteLine;

        /// <inheritdoc />
        public string Name { get; set; } = "console";

        /// <inheritdoc />
        public Task ProcessAsync(IObservable<object> source)
        {
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(WriteFunction, exception => tcs.SetException(exception), () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}