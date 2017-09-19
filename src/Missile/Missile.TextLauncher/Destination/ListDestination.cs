using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<ListDestinationItem>
    {
        public string Name { get; set; } = "list";

        //[Import]
        //public IUiFacade UiFacade { get; set; }

        public Task ProcessAsync(IObservable<ListDestinationItem> source)
        {
            IDestination<ListDestinationItem> x = new ListDestination();
            //UiFacade.SetOutputControl(new ListOutputControl());
            return Task.CompletedTask;
        }
    }

    public class ListDestinationItem
    {
        private string v;

        public ListDestinationItem(string v)
        {
            this.v = v;
        }
    }
}