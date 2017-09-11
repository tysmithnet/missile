using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IConverter))]
    public class ListDestinationConverter : IConverter<object, ListDestinationItem>
    {
        public IObservable<ListDestinationItem> Convert(IObservable<object> source)
        {
            return source.Select(x => new ListDestinationItem(x.ToString()));
        }
    }
}