using System;

namespace Missile.TextLauncher
{
    public abstract class Provider<TDest> where TDest : class
    {   
        public abstract string Name { get; set; }
        public abstract IObservable<TDest> Provide();
    }
}