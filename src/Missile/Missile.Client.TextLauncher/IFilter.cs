using System;

namespace Missile.Client.TextLauncher
{
    public interface IFilter<in TSource, out TDest>
    {
        IObservable<TDest> Filter(IObservable<TSource> source);
    }
}