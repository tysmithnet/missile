using System;
using System.Collections.Generic;

namespace Missile.TextLauncher
{
    public interface IConverterSelectionStrategy
    {
        IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters, Type source,
            Type dest);
    }
}