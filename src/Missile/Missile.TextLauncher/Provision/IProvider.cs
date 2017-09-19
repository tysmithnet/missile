using System;

namespace Missile.TextLauncher.Provision
{
    public interface IProvider
    {
    }

    public interface IProvider<out TDest> : IProvider
    {
        string Name { get; set; }
        IObservable<TDest> Provide(string[] args);
    }
}