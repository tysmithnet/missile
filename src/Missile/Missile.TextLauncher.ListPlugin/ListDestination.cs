using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.ListPlugin
{
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<ListDestinationItem>
    {
        [Import]
        public IUiFacade UiFacade { get; set; }

        public string Name { get; set; } = "list";
        public Task ProcessAsync(IObservable<ListDestinationItem> source)
        {
            throw new NotImplementedException();
        }
    }

    public class ListDestinationItem
    {
        
    }
}
