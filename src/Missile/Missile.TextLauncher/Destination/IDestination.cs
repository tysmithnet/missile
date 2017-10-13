using System;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Tagging interface for Destinations
    /// </summary>
    public interface IDestination
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Represents an object that can accept observables of some type and process them, and
    ///     then return a Task that will represent the completion of the processing.
    /// </summary>
    /// <typeparam name="TSource">The source type of the observable being fed to this destination</typeparam>
    public interface IDestination<in TSource> : IDestination
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Processes the input items asynchronously
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task that when complete will signal the completion of the processing
        /// </returns>
        Task ProcessAsync(IObservable<TSource> source, CancellationToken cancellationToken);
    }
}