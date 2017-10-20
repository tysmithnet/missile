using System;
using System.Diagnostics.CodeAnalysis;
using Missile.TextLauncher;

namespace Missile.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Command instructing the application repository state to be saved
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommand" />
    public class SaveApplicationRepositoryStateCommand : ICommand
    {
        [ExcludeFromCodeCoverage]
        public Guid Id { get; } = Guid.NewGuid();
    }
}