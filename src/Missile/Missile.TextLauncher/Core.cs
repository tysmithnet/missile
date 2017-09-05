using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public class Core
    {
    }

    public interface IProvider<out TDest>
    {
        IObservable<TDest> Provide();
    }

    public interface IFilter<in TSource, out TDest>
    {
        IObservable<TDest> Filter(IObservable<TSource> source);
    }

    public interface IDestination<in TSource>
    {
        Task Process(IObservable<TSource> source);
    }

    public interface IConverter
    {
        bool CanHandle(Type source, Type dest);
        object Convert(object source);
    }
}
