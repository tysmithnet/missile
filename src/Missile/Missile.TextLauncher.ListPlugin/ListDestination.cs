using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.ListPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Destination that displays items in a some sort of rectangular arrangement
    ///     Think of the different folder views in explorer
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Destination.IDestination{Missile.TextLauncher.ListPlugin.IListDestinationItem}" />
    [Export(typeof(IDestination))]
    public class ListDestination : IDestination<IListDestinationItem>
    {
        /// <summary>
        ///     Gets or sets the UI facade
        /// </summary>
        /// <value>
        ///     The UI facade
        /// </value>
        [Import]
        protected internal IUiFacade UiFacade { get; set; }

        /// <summary>
        ///     Gets or sets the command hub
        /// </summary>
        /// <value>
        ///     The command hub
        /// </value>
        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        /// <summary>
        ///     Gets or sets the context menu providers
        /// </summary>
        /// <value>
        ///     The context menu providers
        /// </value>
        [ImportMany]
        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        /// <value>
        ///     The name
        /// </value>
        public string Name { get; set; } = "list";

        /// <inheritdoc />
        /// <summary>
        ///     Process the source items asynchronously
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A Task that when complete will signal the end of the processing</returns>
        public Task ProcessAsync(IObservable<IListDestinationItem> source, CancellationToken cancellationToken)
        {
            var outputControl = new ListDestinationOutput(source, ContextMenuProviders);
            UiFacade.SetOutputControl(outputControl);
            var tcs = new TaskCompletionSource<object>();

            CommandHub.Get<RemoveListDestinationItemCommand>()
                .Subscribe(command => outputControl.Remove(command.ListDestinationItem));

            source.Subscribe(item =>
                {
                    ;
                },
                exception => { tcs.TrySetException(exception); }, () => { tcs.TrySetResult(null); });
            return tcs.Task;
        }
    }
}