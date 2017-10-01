using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    /// Represents a filter that will only provide up to a maximum number of values
    /// </summary>
    [Export(typeof(IFilter<object, object>))]
    public class TakeFilter : IFilter<object, object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "take";

        /// <inheritdoc />
        public IObservable<object> Process(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}