using System.Windows.Controls;

namespace Missile.TextLauncher
{
    public interface IDestinationContextMenuProvider<in TSource>
    {
        bool CanHandle(TSource item);
        MenuItem GetMenuItem(TSource item);
    }
}