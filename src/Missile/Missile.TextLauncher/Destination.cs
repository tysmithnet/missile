using System;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public abstract class Destination<TSource> where TSource : class
    {
        public abstract Task ProcessAsync(IObservable<TSource> source);
    }
}