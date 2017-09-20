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
            var outputControl = new ListOutputControl(items);
            UiFacade.SetOutputControl(outputControl);
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            source.Subscribe(item =>
            {
                UiFacade.Post(x => outputControl.Items.Add(item), null);
            }, exception =>
            {
                tcs.TrySetException(exception);
            }, () =>
            {
                tcs.TrySetResult(null);
            });
            return tcs.Task;
        }
    }

    public class ListDestinationItem
    {
        public ListDestinationItem(string text)
        {
            MainText = text;
        }

        public string MainText { get; set; }
    }
}