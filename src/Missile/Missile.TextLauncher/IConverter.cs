using System;

namespace Missile.TextLauncher
{
    public interface IConverter
    {
    }

    public interface IConverter<in TSource, out TDest> : IConverter
    {
        IObservable<TDest> Convert(IObservable<TSource> source);
    }
}