using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Filter that does not allow duplicate values to pass through
    /// </summary>
    [Export(typeof(IFilter<object, object>))]
    public class DistinctFilter : IFilter<object, object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "distinct";

        /// <inheritdoc />
        public IObservable<object> Process(IObservable<object> source)
        {
            return source.Distinct();
        }
    }
}