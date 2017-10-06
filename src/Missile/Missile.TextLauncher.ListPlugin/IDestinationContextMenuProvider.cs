using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    public interface IDestinationContextMenuProvider<in TSource>
    {
        bool CanHandle(TSource item);
        MenuItem GetMenuItem(TSource item, IListDestinationItem target);
    }
}