using System.Collections.Generic;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    public interface IDestinationContextMenuProvider
    {
        bool CanHandle(IEnumerable<object> items);
        IEnumerable<MenuItem> GetMenuItems(IEnumerable<object> items);
    }
}