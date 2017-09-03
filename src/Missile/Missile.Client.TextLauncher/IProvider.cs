using System;

namespace Missile.Client.TextLauncher
{
    public interface IProvider
    {
        string Name { get; }
        IObservable<object> Get(string argString);
    }

    public interface IProvider<out TDest> : IProvider
    {
        new IObservable<TDest> Get(string argString);
    }
}