using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <inheritdoc />
    /// <summary>
    ///     Destination that will output source items to the Console
    /// </summary>
    /// <seealso cref="T:System.Object" />
    [Export(typeof(IDestination))]
    public class ConsoleDestination : IDestination<object>
    {
        /// <summary>
        ///     Function to invoke when writing to Console
        /// </summary>
        /// <value>
        ///     The write function.
        /// </value>
        protected internal virtual Action<object> WriteFunction { get; set; } = Console.WriteLine;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; } = "console";

        /// <summary>
        ///     Process the source items asynchronously
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public Task ProcessAsync(IObservable<object> source, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(WriteFunction, exception => tcs.SetException(exception), () => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}