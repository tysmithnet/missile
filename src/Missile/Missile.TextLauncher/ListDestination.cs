using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(Destination<object>))]
    public class ListDestination : Destination<IListDestinationItem>
    {
        public override string Name { get; set; } = "list";

        //[Import]
        //public IUiFacade UiFacade { get; set; }

        public override Task ProcessAsync(IObservable<IListDestinationItem> source)
        {
            Destination<IListDestinationItem> x = new ListDestination();
            //UiFacade.SetOutputControl(new ListOutputControl());
            return Task.CompletedTask;
            
        }
    }

    public interface IListDestinationItem
    {
    }
}