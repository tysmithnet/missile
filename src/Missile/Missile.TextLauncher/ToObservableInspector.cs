using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher
{
    [Export(typeof(IObservableInspector))]
    public class ToObservableInspector : IObservableInspector
    {
        public bool CanHandle(Type type)
        {
            return Regex.IsMatch(type?.FullName ?? "",
                @"^System\.Reactive\.Linq\.ObservableImpl\.ToObservable`1");
        }

        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }
}