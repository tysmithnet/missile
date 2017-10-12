using System;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Command instructing the application repository state to be saved
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommand" />
    public class SaveApplicationRepositoryStateCommand : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}