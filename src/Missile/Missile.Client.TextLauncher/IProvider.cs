using System;

namespace Missile.Client.TextLauncher
{
    public interface IProvider
    {
        string Name { get; }                        
    }

    public interface IProvider<out TDest> : IProvider
    {
        IObservable<TDest> Get(string argString);
    }
}