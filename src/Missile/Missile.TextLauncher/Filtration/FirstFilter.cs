using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    /// Filter that returns the first value and then closes
    /// </summary>
    public class FirstFilter : IFilter<object, object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "first";

        /// <inheritdoc />
        public IObservable<object> Process(IObservable<object> source)
        {
            return source.FirstAsync();
        }
    }
}