using System;

namespace Missile.Client.TextLauncher
{
    public interface IFilter
    {
        IObservable<object> Filter(IObservable<object> source);
    }

    public interface IFilter<in TSource, out TDest> : IFilter
    {
        IObservable<TDest> Filter(IObservable<TSource> source);
    }
}