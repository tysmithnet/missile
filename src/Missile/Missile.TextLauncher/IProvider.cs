using System;

namespace Missile.TextLauncher
{
    public interface IProvider
    {
    }

    public interface IProvider<out TDest> : IProvider
    {
        string Name { get; set; }
        IObservable<TDest> Provide(string argString);
    }
}