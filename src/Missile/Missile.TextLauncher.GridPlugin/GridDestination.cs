using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.GridPlugin
{
    [Export(typeof(IDestination))]
    public class GridDestination : IDestination<GridDestinationItem>
    {
        
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
