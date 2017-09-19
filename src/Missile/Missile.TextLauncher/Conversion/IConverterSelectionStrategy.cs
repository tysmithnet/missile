using System;
using System.Collections.Generic;

namespace Missile.TextLauncher.Conversion
{
    public interface IConverterSelectionStrategy
    {
        IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters, Type source,
            Type dest);
    }
}