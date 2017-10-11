using System;

namespace Missile.TextLauncher
{
    public interface IObservableInspector
    {
        bool CanHandle(Type type);
        Type GetObservableType(Type type);
    }
}