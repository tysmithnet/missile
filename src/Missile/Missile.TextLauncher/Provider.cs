using System;

namespace Missile.TextLauncher
{
    public abstract class Provider<TDest> where TDest : class
    {                                       
        public abstract IObservable<TDest> Provide();
    }
}