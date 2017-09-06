using System;

namespace Missile.TextLauncher
{
    public abstract class Provider<TDest>
    {                                       
        public abstract IObservable<TDest> Provide();
    }
}