using System;

namespace Missile.TextLauncher.Provision
{
    // todo: move to core along with the other corish features
    public interface IProvider
    {
    }

    public interface IProvider<out TDest> : IProvider
    {
        string Name { get; set; }
        IObservable<TDest> Provide(string[] args);
    }
}