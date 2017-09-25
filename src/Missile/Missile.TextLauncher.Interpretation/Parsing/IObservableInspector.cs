using System;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public interface IObservableInspector
    {
        bool CanHandle(Type type);
        Type GetObservableType(Type type);
    }
}