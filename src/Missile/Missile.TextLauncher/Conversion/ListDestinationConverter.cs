using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Destination.ListDestination;

namespace Missile.TextLauncher.Conversion
{
    [Export(typeof(IConverter))]
    public class ListDestinationConverter : IConverter<object, ListDestinationItem>
    {
        public IObservable<ListDestinationItem> Convert(IObservable<object> source)
        {
            return source.Select(x => new ListDestinationItem(){MainText = x.ToString() });
        }
    }
}