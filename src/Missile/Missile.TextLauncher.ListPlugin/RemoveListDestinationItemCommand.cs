using System;

namespace Missile.TextLauncher.ListPlugin
{
    public class RemoveListDestinationItemCommand : ICommand
    {
        public RemoveListDestinationItemCommand(IListDestinationItem target)
        {
            ListDestinationItem = target;
        }

        public IListDestinationItem ListDestinationItem { get; set; }

        public Guid Id { get; } = Guid.NewGuid();
    }
}