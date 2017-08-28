using System;

namespace Missile.Client.TextLauncher
{
    public interface IDestination<in TSource>
    {
        void Process(IObservable<TSource> source);
    }
}