using System;
using System.Collections.Generic;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IConverterRepository
    {
        IEnumerable<ConverterEntry> Get(Type sourceType, Type destType);
    }
}