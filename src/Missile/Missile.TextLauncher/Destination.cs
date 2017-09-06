using System;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public abstract class Destination<TSource>
    {
        public abstract Task ProcessAsync(IObservable<TSource> source);
    }
}