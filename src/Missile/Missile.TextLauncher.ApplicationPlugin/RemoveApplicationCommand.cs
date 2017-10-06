using System;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class RemoveApplicationCommand : ICommand
    {
        public RegisteredApplication RegisteredApplication { get; set; }

        public RemoveApplicationCommand(RegisteredApplication item)
        {
            RegisteredApplication = item;
        }

        public Guid Id { get; } = Guid.NewGuid();
    }
}