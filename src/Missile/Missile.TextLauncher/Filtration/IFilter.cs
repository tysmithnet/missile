using System;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    /// Tagging interface for filters
    /// </summary>
    public interface IFilter
    {
    }

    /// <summary>
    /// Represents an object is capable of transforming an observable
    /// </summary>
    /// <typeparam name="TSource">Source type of the observalbe e.g. IObservable<string> -> string</typeparam>
    /// <typeparam name="TDest">Destination type of the observalbe e.g. IObservable<string> -> string</typeparam>
    public interface IFilter<in TSource, out TDest> : IFilter
    {
        string Name { get; set; }
        IObservable<TDest> Process(IObservable<TSource> source);
    }
}