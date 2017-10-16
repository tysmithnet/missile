using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of ICommandHub
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommandHub" />
    [Export(typeof(ICommandHub))]
    [ExcludeFromCodeCoverage] // not worth testing until it has functionality
    public class CommandHub : ICommandHub
    {
        /// <summary>
        ///     Gets or sets the source of all dispatched commands
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        protected internal SubjectBase<ICommand> Source { get; set; } = new Subject<ICommand>();

        /// <inheritdoc />
        /// <summary>
        ///     Broadcasts the specified command
        /// </summary>
        /// <param name="command">The command to broadcast</param>
        public void Broadcast(ICommand command)
        {
            Source.OnNext(command);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets an observable sequence of commands matching the type parameter
        /// </summary>
        /// <typeparam name="TCommand">The type of the command to get an observable sequence of</typeparam>
        /// <returns>
        ///     An observable sequence of commands matching the type parameter
        /// </returns>
        public IObservable<TCommand> Get<TCommand>() where TCommand : ICommand
        {
            return Source.OfType<TCommand>();
        }
    }
}