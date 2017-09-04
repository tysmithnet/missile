using System;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IConverter
    {
        
    }

    public interface IConverter<in TSource, out TDest> : IConverter
    {
        IObservable<TDest> Convert(IObservable<TSource> source);
    }
}