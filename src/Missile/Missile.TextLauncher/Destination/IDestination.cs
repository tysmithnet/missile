using System;
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
        string Name { get; set; }
        Task ProcessAsync(IObservable<TSource> source);
    }
}