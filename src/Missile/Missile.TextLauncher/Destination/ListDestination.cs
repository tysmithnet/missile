using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<ListDestinationItem>
    {
        [Import]
        public IUiFacade UiFacade { get; set; }

        public string Name { get; set; } = "list";

        public Task ProcessAsync(IObservable<ListDestinationItem> source)
        {
            var items = source.ToEnumerable();
            UiFacade.SetOutputControl(new ListOutputControl(items));
            return Task.CompletedTask;
        }
    }

    public class ListDestinationItem
    {
        public string MainText { get; set; }

        public ListDestinationItem(string text)
        {
            MainText = text;
        }
    }
}