using System;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class RemoveApplicationCommand : ICommand
    {
        public RemoveApplicationCommand(RegisteredApplication item)
        {
            RegisteredApplication = item;
        }

        public RegisteredApplication RegisteredApplication { get; set; }

        public Guid Id { get; } = Guid.NewGuid();
    }
}