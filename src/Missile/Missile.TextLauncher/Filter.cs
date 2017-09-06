using System;

namespace Missile.TextLauncher
{
    public abstract class Filter<TSource, TDest> where TSource : class where TDest : class
    {
        public abstract IObservable<TDest> Process(IObservable<TSource> source);
    }
}