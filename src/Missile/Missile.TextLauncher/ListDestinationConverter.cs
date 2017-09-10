using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    [Export(typeof(IConverter))]
    public class ListDestinationConverter : IConverter
    {
        public Func<object, ListDestinationItem> ConversionFunc { get; set; } =
            o => new ListDestinationItem();

        public bool CanHandleRequest(Type sourceType, Type destType)
        {
            if (destType != typeof(IObservable<ListDestinationItem>))
                return false;

            bool isSameSource = typeof(IObservable<object>) == sourceType;
            bool sourceHasInterface = sourceType.GetInterfaces().Any(i => i == typeof(IObservable<object>));
            if (!isSameSource && !sourceHasInterface)
                return false;
            return true;
        }

        public object Convert(object source, Type sourceType, Type destType)
        {   
            return ((IObservable<object>) source).Select(ConversionFunc);
        }
    }
}
