using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.ListPlugin
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<IListDestinationItem>
    {
        [Import]
        protected internal IUiFacade UiFacade { get; set; }

        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        public string Name { get; set; } = "list";

        public Task ProcessAsync(IObservable<IListDestinationItem> source)
        {
            var outputControl = new ListDestinationOutput(source);
            UiFacade.SetOutputControl(outputControl);
            var tcs = new TaskCompletionSource<object>();
            var syncContext = SynchronizationContext.Current;
            Task.Factory.StartNew(() =>
            {
                CommandHub.Get<RemoveListDestinationItemCommand>().SubscribeOn(syncContext).ForEachAsync(x => outputControl.Remove(x.ListDestinationItem));
            });                                                                                                              

            source.Subscribe(item =>
                {
                    ;
                },
                exception => { tcs.TrySetException(exception); }, () => { tcs.TrySetResult(null); });
            return tcs.Task;
        }
    }
}