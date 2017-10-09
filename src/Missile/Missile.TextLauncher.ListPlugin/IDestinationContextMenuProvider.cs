using System.Collections.Generic;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    public interface IDestinationContextMenuProvider
    {
        IEnumerable<MenuItem> GetMenuItems(IEnumerable<IListDestinationItem> items);
    }
}