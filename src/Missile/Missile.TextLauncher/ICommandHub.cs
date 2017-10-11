using System;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents a central location for command dispatching and subscription
    /// </summary>
    public interface ICommandHub
    {
        /// <summary>
        ///     Broadcasts the specified command
        /// </summary>
        /// <param name="command">The command to broadcast</param>
        void Broadcast(ICommand command);


        /// <summary>
        ///     Gets an observable sequence of commands matching the type parameter
        /// </summary>
        /// <typeparam name="TCommand">The type of the command to get an observable sequence of</typeparam>
        /// <returns>An observable sequence of commands matching the type parameter</returns>
        IObservable<TCommand> Get<TCommand>() where TCommand : ICommand;
    }
}