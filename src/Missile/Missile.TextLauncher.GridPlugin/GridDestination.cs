using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.GridPlugin
{
    [Export(typeof(IDestination))]
    public class GridDestination : IDestination<GridDestinationItem>
    {
        [Import]
        public IUiFacade UiFacade { get; set; }

        public string Name { get; set; } = "grid";

        public Task ProcessAsync(IObservable<GridDestinationItem> source)
        {
            throw new NotImplementedException();
        }
    }

    public class GridDestinationItem : UserControl
    {
    }
}