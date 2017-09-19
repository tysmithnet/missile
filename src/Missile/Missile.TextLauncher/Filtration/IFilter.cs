using System;

namespace Missile.TextLauncher.Filtration
{
    public interface IFilter
    {
    }

    public interface IFilter<in TSource, out TDest> : IFilter
    {
        string Name { get; set; }
        IObservable<TDest> Process(IObservable<TSource> source);
    }
}