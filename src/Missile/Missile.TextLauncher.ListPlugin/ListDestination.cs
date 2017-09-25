using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.ListPlugin
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<UserControl>
    {
        [Import]
        public IUiFacade UiFacade { get; set; }

        public string Name { get; set; } = "list";

        public Task ProcessAsync(IObservable<UserControl> source)
        {                                        
            var outputControl = new ListDestinationOutput(source);
            UiFacade.SetOutputControl(outputControl);
            var tcs = new TaskCompletionSource<object>();
            source.Subscribe(item =>
                {
                    ;
                },
                exception =>
                {
                    tcs.TrySetException(exception);
                }, () =>
                {
                    tcs.TrySetResult(null);
                });
            return tcs.Task; 
        }
    }
}