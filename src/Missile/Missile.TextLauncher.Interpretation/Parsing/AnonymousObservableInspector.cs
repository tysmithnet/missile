using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    [Export(typeof(IObservableInspector))]
    public class AnonymousObservableInspector : IObservableInspector
    {
        public bool CanHandle(Type type)
        {
            return Regex.IsMatch(type?.FullName ?? "",
                @"^System\.Reactive\.AnonymousObservable`1");
        }

        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }
}