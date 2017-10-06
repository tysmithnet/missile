using System;

namespace Missile.TextLauncher.ListPlugin
{
    public class RemoveListDestinationItemCommand : ICommand
    {
        public IListDestinationItem ListDestinationItem { get; set; }

        public RemoveListDestinationItemCommand(IListDestinationItem target)
        {
            ListDestinationItem = target;
        }

        public Guid Id { get; } = Guid.NewGuid();
    }
}