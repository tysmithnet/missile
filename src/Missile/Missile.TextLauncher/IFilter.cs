using System;

namespace Missile.TextLauncher
{
    public interface IFilter
    {
        
    }

    public interface IFilter<in TSource, out TDest> : IFilter
    {
        IObservable<TDest> Process(IObservable<TSource> source);
    }
}