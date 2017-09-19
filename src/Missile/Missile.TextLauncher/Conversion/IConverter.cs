using System;

namespace Missile.TextLauncher.Conversion
{
    public interface IConverter
    {
    }

    public interface IConverter<in TSource, out TDest> : IConverter
    {
        IObservable<TDest> Convert(IObservable<TSource> source);
    }
}