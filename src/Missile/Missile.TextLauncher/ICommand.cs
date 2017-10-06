using System;

namespace Missile.TextLauncher
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}