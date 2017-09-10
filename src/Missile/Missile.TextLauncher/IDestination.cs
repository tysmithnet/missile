using System;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IDestination
    {
        
    }

    public interface IDestination<in TSource> : IDestination
    {
        string Name { get; set; }
        Task ProcessAsync(IObservable<TSource> source);
    }
}