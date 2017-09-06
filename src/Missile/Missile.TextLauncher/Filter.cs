using System;

namespace Missile.TextLauncher
{
    public abstract class Filter<TSource, TDest>
    {
        public abstract IObservable<TDest> Process(IObservable<TSource> source);
    }
}