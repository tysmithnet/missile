using System;

namespace Missile.Client.TextLauncher
{
    public interface IDestination
    {                                              
        string Name { get; }
    }

    public interface IDestination<in TSource> : IDestination
    {
        void Process(IObservable<TSource> source);
    }
}