using System;

namespace Missile.Client.TextLauncher
{
    public interface IDestination
    {
        void Process(IObservable<object> source);
    }

    public interface IDestination<in TSource> : IDestination
    {
        void Process(IObservable<TSource> source);
    }
}