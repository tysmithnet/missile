using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<IListDestinationItem>
    {
        public string Name { get; set; } = "list";

        //[Import]
        //public IUiFacade UiFacade { get; set; }

        public Task ProcessAsync(IObservable<IListDestinationItem> source)
        {
            IDestination<IListDestinationItem> x = new ListDestination();
            //UiFacade.SetOutputControl(new ListOutputControl());
            return Task.CompletedTask;
            
        }
    }

    public interface IListDestinationItem
    {
    }
}