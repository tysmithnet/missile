using System;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher
{
    public interface IDestination
    {                                              
        string Name { get; }
    }

    public interface IDestination<in TSource> : IDestination
    {
        Task Process(IObservable<TSource> source);
    }
}