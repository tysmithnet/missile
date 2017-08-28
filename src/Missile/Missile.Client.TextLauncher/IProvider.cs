using System;

namespace Missile.Client.TextLauncher
{
    public interface IProvider
    {
        IObservable<object> Get(string[] args);
    }
}