using System.Windows.Controls;

namespace Missile.TextLauncher
{
    public interface IDestinationContextMenuProvider
    {

    }        

    public interface IDestinationContextMenuProvider<in TSource> : IDestinationContextMenuProvider
    {
        bool CanHandle(TSource item);
        MenuItem GetMenuItem(TSource item);
    }
}