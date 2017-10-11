using System;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents a command that can be broadcast to interested parties
    /// </summary>
    public interface ICommand
    {
        Guid Id { get; }
    }
}