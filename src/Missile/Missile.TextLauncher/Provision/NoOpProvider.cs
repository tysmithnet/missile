using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Provision
{
    /// <inheritdoc />
    /// <summary>
    ///     A provider that immediately completes the observable sequence
    /// </summary>
    /// <seealso cref="T:System.Object" />
    [Export(typeof(IProvider))]
    public class NoOpProvider : IProvider<object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "noop";

        /// <inheritdoc />
        public IObservable<object> Provide(string[] args)
        {
            return new object[0].ToObservable();
        }
    }
}