using System;

namespace Missile.Client.TextLauncher
{
    public interface IProvider<out TDest>
    {
        IObservable<TDest> Get(string[] args);
    }
}