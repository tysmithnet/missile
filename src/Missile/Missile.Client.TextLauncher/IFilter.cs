using System;

namespace Missile.Client.TextLauncher
{
    public interface IFilter
    {                                                           
        string Name { get; }
    }

    public interface IFilter<in TSource, out TDest> : IFilter
    {
        IObservable<TDest> Filter(IObservable<TSource> source);
    }
}